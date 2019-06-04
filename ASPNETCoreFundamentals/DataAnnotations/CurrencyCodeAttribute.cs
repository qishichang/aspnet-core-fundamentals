using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.DataAnnotations
{
    public class CurrencyCodeAttribute : ValidationAttribute
    {
        private readonly string[] _allowedCodes;

        public CurrencyCodeAttribute(params string[] allowedCodes)
        {
            _allowedCodes = allowedCodes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var code = value as string;
            if (code == null || !_allowedCodes.Contains(code))
            {
                return new ValidationResult("Not a valid currency code");
            }

            return ValidationResult.Success;
        }
    }
}
