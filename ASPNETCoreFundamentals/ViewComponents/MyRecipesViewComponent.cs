using ASPNETCoreFundamentals.Models;
using ASPNETCoreFundamentals.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.ViewComponents
{
    public class MyRecipesViewComponent : ViewComponent
    {
        private readonly RecipeService _recipeService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyRecipesViewComponent(RecipeService recipeService,
            UserManager<ApplicationUser> userManager)
        {
            _recipeService = recipeService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int numberOfRecipes)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("Unauthenticated");
            }

            var userId = _userManager.GetUserId(HttpContext.User);
            var recipes = _recipeService.GetRecipesForUser(
                userId, numberOfRecipes);
            return View(recipes);
        }
    }
}
