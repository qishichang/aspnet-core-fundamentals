using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Models
{
    public class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public string NationalFlagPath { get; set; } = "Images/Default.png";

        public Country(string code, string name, string continent)
        {
            this.Code = code;
            this.Name = name;
            this.Continent = continent;
        }

    }
}
