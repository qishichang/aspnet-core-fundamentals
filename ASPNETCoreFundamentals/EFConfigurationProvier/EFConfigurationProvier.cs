using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Data;
using ASPNETCoreFundamentals.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ASPNETCoreFundamentals.EFConfigurationProvier
{
    public class EFConfigurationProvier : ConfigurationProvider
    {
        public EFConfigurationProvier(Action<DbContextOptionsBuilder> optionsAction)
        {
            OptionsAction = optionsAction;
        }

        public Action<DbContextOptionsBuilder> OptionsAction { get; }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<EFConfigurationContext>();
            OptionsAction(builder);

            using (var dbContext = new EFConfigurationContext(builder.Options))
            {
                dbContext.Database.EnsureCreated();

                Data = !dbContext.Values.Any() ? CreateAndSaveDefaultValues(dbContext) : dbContext.Values.ToDictionary(c => c.Id, c => c.Value);
               
            }
        }

        private static IDictionary<string, string> CreateAndSaveDefaultValues(EFConfigurationContext dbContext)
        {
            var configValues = new Dictionary<string, string>
            {
                { "quote1", "I aim to misbehave." },
                { "quote2", "I swallowed a bug." },
                { "quote3", "You can't stop the signal, Mal." }
            };

            dbContext.Values.AddRange(configValues
                .Select(kvp => new EFConfigurationValue
                {
                    Id = kvp.Key,
                    Value = kvp.Value
                }).ToArray());
            dbContext.SaveChanges();

            return configValues;
        }
    }
}
