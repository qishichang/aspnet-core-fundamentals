using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreFundamentals.Controllers
{
    public class RecipeController : Controller
    {
        private readonly RecipeService _service;

        public RecipeController(RecipeService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}