using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Authorization
{
    public class BannedFromLoungeHandler : AuthorizationHandler<AllowedInLoungeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AllowedInLoungeRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "IsBanned"))
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
