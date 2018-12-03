using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Models
{
    public class Discount
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Rate { get; set; }
    }
}
