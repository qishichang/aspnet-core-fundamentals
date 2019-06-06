using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Helpers;
using ASPNETCoreFundamentals.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreFundamentals.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : Controller
    {
        private readonly CurrencyConverter _converter;

        public CurrencyController(CurrencyConverter converter)
        {
            _converter = converter;
        }

        [HttpPost]
        public IActionResult Convert(ConvertInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _converter.ConvertToGbp(
                model.Value, 
                model.ExchangeRate, 
                model.DecimalPlaces);

            return Json(new { Result = result });
        }
    }
}