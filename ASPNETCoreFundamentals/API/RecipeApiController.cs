using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Filters;
using ASPNETCoreFundamentals.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreFundamentals.API
{
    [Route("api/recipe")]
    [ApiController]
    [FeatureEnabled(IsEnabled = true)]
    [ValidateModel]
    [HandleException]
    public class RecipeApiController : ControllerBase
    {
        private const bool IsEnabled = true;
        private readonly RecipeService _service;

        public RecipeApiController(RecipeService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        [EnsureRecipeExists]
        [AddLastModifiedHeader]
        public IActionResult Get(int id)
        {
            var detail = _service.GetRecipeDetail(id);
            return Ok(detail);

        }

        [HttpPost("{id}")]
        [EnsureRecipeExists]
        public IActionResult Edit(int id, UpdateRecipeCommand command)
        {
            _service.UpdateRecipe(command);
            return Ok();
        }
    }
}