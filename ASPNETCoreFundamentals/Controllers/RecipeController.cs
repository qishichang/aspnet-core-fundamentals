using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreFundamentals.Controllers
{
    public class RecipeController : Controller
    {
        private readonly RecipeService _service;
        private readonly ILogger<RecipeService> _log;

        public RecipeController(RecipeService service,
                ILogger<RecipeService> log)
        {
            _service = service;
            _log = log;
        }
        public IActionResult Index()
        {
            var models = _service.GetRecipes();
            _log.LogInformation("Loaded {RecipeCount} recipes", models.Count);
            return View(models);
        }
    }
}