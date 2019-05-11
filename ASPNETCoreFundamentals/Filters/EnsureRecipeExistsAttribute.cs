using ASPNETCoreFundamentals.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Filters
{
    public class EnsureRecipeExistsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var service = (RecipeService)context.HttpContext.RequestServices.GetService(typeof(RecipeService));
            var recipeId = (int)context.ActionArguments["id"];
            if (!service.DoesRecipeExist(recipeId))
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}
