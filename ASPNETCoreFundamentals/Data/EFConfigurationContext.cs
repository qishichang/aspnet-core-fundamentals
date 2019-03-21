using ASPNETCoreFundamentals.Options;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Data
{
    public class EFConfigurationContext : DbContext
    {
        public EFConfigurationContext(DbContextOptions<EFConfigurationContext> options) : base(options)
        {
        }

        public DbSet<EFConfigurationValue> Values { get; set; }
    }
}
