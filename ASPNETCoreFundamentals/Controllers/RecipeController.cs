using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreFundamentals.Controllers
{
    public class RecipeController : Controller
    {
        private readonly RecipeService _service;
        private readonly IAuthorizationService _authService;
        private readonly ILogger<RecipeService> _log;

        public RecipeController(RecipeService service,
                IAuthorizationService authService,
                ILogger<RecipeService> log)
        {
            _service = service;
            _authService = authService;
            _log = log;
        }
        public IActionResult Index()
        {
            var models = _service.GetRecipes();
            _log.LogInformation("Loaded {RecipeCount} recipes", models.Count);
            return View(models);
        }

        public async Task<IActionResult> View(int id)
        {
            _log.LogInformation("Loading recipe with id {RecipeId}", id);
            var model = _service.GetRecipeDetail(id);
            if(model == null)
            {
                _log.LogWarning("Could not find recipe with id {RecipeId}", id);
                return NotFound();
            }

            var recipe = _service.GetRecipe(id);
            var authResult = await _authService.AuthorizeAsync(User, recipe, "CanManageRecipe");
            model.CanEditRecipe = authResult.Succeeded;
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = _service.GetRecipe(id);
            var authResult = await _authService.AuthorizeAsync(User, recipe, "CanManageRecipe");
            if (!authResult.Succeeded)
            {
                return Forbid();
            }
            return View(recipe);
        }
    }
}