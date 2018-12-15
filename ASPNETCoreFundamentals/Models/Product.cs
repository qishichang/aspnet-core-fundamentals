using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public string Description { get; set; }

        public Product()
        {

        }

        public Product(int id, string name, double price)
        {
            this.ID = id;
            this.Name = name;
            this.Price = price;
        }
    }
}
