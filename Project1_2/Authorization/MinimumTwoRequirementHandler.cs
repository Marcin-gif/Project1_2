using Microsoft.AspNetCore.Authorization;
using Project1_2.Entities;
using Project1_2.Services;
using System.Security.Claims;

namespace Project1_2.Authorization
{
    public class MinimumTwoRequirementHandler : AuthorizationHandler<MinimumTwoRequirement>
    {
        private readonly ILogger<MinimumTwoRequirementHandler> _logger;
        private readonly RestaurantDbContext _dbContext;
        
        public MinimumTwoRequirementHandler(ILogger<MinimumTwoRequirementHandler> logger,RestaurantDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            MinimumTwoRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var createdRestaurantById = _dbContext.restaurants
                .Count(c => c.CreatedById == userId);
            if (createdRestaurantById >= requirement.MinimumTwo)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
