using ASPNETCoreFundamentals.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Models
{
    public class CurrencyConverterModel
    {
        [Required]
        [StringLength(3, MinimumLength = 3)]
        [CurrencyCode]
        public string CurrencyFrom { get; set; }

        [Required]
        [CurrencyCode]
        [StringLength(3, MinimumLength = 3)]
        public string CurrencyTo { get; set; }

        [Required]
        [Range(1, 1000)]
        public decimal Quantity { get; set; }
    }
}
