using Microsoft.AspNetCore.Authorization;

namespace Project1_2.Authorization
{
    public class MinimumTwoRequirement : IAuthorizationRequirement
    {
        public int MinimumTwo { get; }
        public MinimumTwoRequirement(int minimumTwo) 
        { 
            MinimumTwo = minimumTwo;
        }
    }
}
