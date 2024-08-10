
using System.Diagnostics;

namespace Project1_2.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private Stopwatch _stopWatch;
        private readonly ILogger<RequestTimeMiddleware> _logger;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger) 
        { 
            _stopWatch = new Stopwatch();
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopWatch.Start();
           await next.Invoke(context);

            _stopWatch.Stop();
           var elapsedMilliSeconds = _stopWatch.ElapsedMilliseconds;
            if (elapsedMilliSeconds / 1000 > 4)
            {
                var messeage = $"Request {context.Request.Method} at {context.Request.Path} took {elapsedMilliSeconds} ms";
                
                _logger.LogInformation(messeage);
            }
        }
    }
}
