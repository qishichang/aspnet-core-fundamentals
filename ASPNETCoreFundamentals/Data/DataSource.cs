using ASPNETCoreFundamentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Data
{
    public class DataSource
    {
        public static IList<Country> Countries { get; }
        static DataSource()
        {
            Countries = new List<Country>();
            Countries.Add(new Country("USA", "United States", "North America"));
            Countries.Add(new Country("CAN", "Canada", "North America"));
            Countries.Add(new Country("CHN", "China", "Asia"));
            Countries.Add(new Country("IND", "India", "Asia"));
        }
    }
}
