using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Models
{
    public class ProductListViewModel
    {
        public Discount Discount { get; set; }
        public IList<Product> Products { get; set; }
    }
}
