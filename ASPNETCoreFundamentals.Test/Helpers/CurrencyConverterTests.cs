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
        [Theory]
        [InlineData(0, 3, 0)]
        [InlineData(3, 1.5, 2)]
        [InlineData(3.75, 2.5, 1.5)]
        public void ConvertToGbp_ConvertsCorrectly(decimal value, decimal rate, decimal expected)
        {
            // Arrange
            var converter = new CurrencyConverter();
            int dps = 4;

            // Act
            var actual = converter.ConvertToGbp(value, rate, dps);

            // Assert
            actual.ShouldBe(expected);
        }
    }
}
