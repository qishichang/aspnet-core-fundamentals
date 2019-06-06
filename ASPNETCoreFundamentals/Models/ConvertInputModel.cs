using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Models
{
    public class ConvertInputModel
    {
        [DisplayName("Value in GBP")]
        public decimal Value { get; set; } = 0;

        [DisplayName("Exchange rate from GBP to alternate currency")]
        [Range(0, double.MaxValue)]
        public decimal ExchangeRate { get; set; }

        [DisplayName("Round to decimal places")]
        [Range(0, int.MaxValue)]
        public int DecimalPlaces { get; set; }
    }
}
