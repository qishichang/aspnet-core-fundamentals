﻿using System;
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
    [EnsureRecipeExists]
    public class RecipeApiController : ControllerBase
    {
        private const bool IsEnabled = true;
        private readonly RecipeService _service;

        public RecipeApiController(RecipeService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var detail = _service.GetRecipeDetail(id);
                Response.GetTypedHeaders().LastModified = detail.LastModified;
                return Ok(detail);
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex);
            }
        }

        [HttpPost("{id}")]
        public IActionResult Edit(int id, UpdateRecipeCommand command)
        {
            try
            {
                _service.UpdateRecipe(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex);
            }
        }

        private IActionResult GetErrorResponse(Exception ex)
        {
            var error = new
            {
                Success = false,
                Errors = new[]
                {
                   ex.Message
               }
            };

            return new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}