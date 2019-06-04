using ASPNETCoreFundamentals.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNETCoreFundamentals.DataAnnotations
{
    public class CurrencyCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var provider = validationContext.GetService<ICurrencyProvider>();
            var allowedCodes = provider.GetCurrencies();
            var code = value as string;
            if (code == null || !allowedCodes.Contains(code))
            {
                return new ValidationResult("Not a valid currency code");
            }

            return ValidationResult.Success;
        }
    }
}
