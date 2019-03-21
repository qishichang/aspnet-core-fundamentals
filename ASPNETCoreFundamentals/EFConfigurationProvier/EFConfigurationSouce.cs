using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.EFConfigurationProvier
{
    public class EFConfigurationSouce : IConfigurationSource
    {
        private readonly Action<DbContextOptionsBuilder> _optionsAction;

        public EFConfigurationSouce(Action<DbContextOptionsBuilder> optionsAction)
        {
            _optionsAction = optionsAction;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EFConfigurationProvier(_optionsAction);
        }
    }
}
