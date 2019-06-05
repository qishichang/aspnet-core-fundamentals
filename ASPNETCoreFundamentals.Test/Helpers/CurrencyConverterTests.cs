using ASPNETCoreFundamentals.Helpers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ASPNETCoreFundamentals.Test.Helpers
{
    public class CurrencyConverterTests
    {
        [Fact]
        public void ConvertToGbp_ConvertsCorrectly()
        {
            // Arrange
            var converter = new CurrencyConverter();
            decimal value = 3;
            decimal rate = 1.5m;
            int dp = 4;

            decimal expected = 2;

            // Act
            var actual = converter.ConvertToGbp(value, rate, dp);

            // Assert
            actual.ShouldBe(expected);
        }
    }
}
