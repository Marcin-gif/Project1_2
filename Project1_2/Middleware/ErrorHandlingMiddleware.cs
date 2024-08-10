
using Project1_2.Exceptions;
using System.Diagnostics;

namespace Project1_2.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                Stopwatch s = Stopwatch.StartNew();
                s.Start();
                await next.Invoke(context);

            }
            catch (ForbidException e)
            { 
                context.Response.StatusCode = 403;
            }
            catch (BadRequestException e)
            {
                context.Response.StatusCode = 400;
                context.Response.WriteAsync(e.Message);
            }
            catch (NotFoundException nfe)
            {
                context.Response.StatusCode = 404;
                context.Response.WriteAsync(nfe.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
