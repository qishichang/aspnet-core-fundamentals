using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Options
{
    public class CurrencyOptions
    {
        public string[] Currencies { get; set; }
        public string DefaultCurrency { get; set; }
    }
}
