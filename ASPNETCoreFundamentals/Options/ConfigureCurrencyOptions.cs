using ASPNETCoreFundamentals.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Options
{
    public class ConfigureCurrencyOptions : IConfigureOptions<CurrencyOptions>
    {
        private readonly ICurrencyProvider _currencyProvider;

        public ConfigureCurrencyOptions(ICurrencyProvider currencyProvider)
        {
            _currencyProvider = currencyProvider;
        }
        public void Configure(CurrencyOptions options)
        {
            options.Currencies = _currencyProvider.GetCurrencies();
        }
    }
}
