using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Authorization
{
    public class FrequentFlyerHandler : AuthorizationHandler<AllowedInLoungeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AllowedInLoungeRequirement requirement)
        {
            if (context.User.HasClaim("FrequentFlyerCard", "Gold"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
