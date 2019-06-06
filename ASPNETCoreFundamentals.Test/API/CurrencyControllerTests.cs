using ASPNETCoreFundamentals.API;
using ASPNETCoreFundamentals.Helpers;
using ASPNETCoreFundamentals.Models;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ASPNETCoreFundamentals.Test.API
{
    public class CurrencyControllerTests
    {
        [Fact]
        public void Convert_ReturnsBadRequestWhenInvalid()
        {
            // Arrange
            var converter = new CurrencyConverter();
            var controller = new CurrencyController(converter);
            var model = new ConvertInputModel
            {
                Value = 1,
                ExchangeRate = -2,
                DecimalPlaces = 2,
            };

            controller.ModelState.AddModelError(
                nameof(model.ExchangeRate),
                "Exchange rate must be greater than zero"
            );

            // Act
            var result = controller.Convert(model);

            // Assert
            result.ShouldBeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void Convert_ReturnsValue()
        {
            // Arrange
            var converter = new CurrencyConverter();
            var controller = new CurrencyController(converter);
            var model = new ConvertInputModel
            {
                Value = 1,
                ExchangeRate = 2,
                DecimalPlaces = 2,
            };

            // Act
            var result = controller.Convert(model);

            // Assert
            result.ShouldBeOfType<JsonResult>();
        }
    }
}
