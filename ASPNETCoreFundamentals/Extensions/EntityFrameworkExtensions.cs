﻿using ASPNETCoreFundamentals.EFConfigurationProvier;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static IConfigurationBuilder AddEFConfiguration(this IConfigurationBuilder builder, Action<DbContextOptionsBuilder> optionsAction)
        {
            return builder.Add(new EFConfigurationSouce(optionsAction));
        }
    }
}
