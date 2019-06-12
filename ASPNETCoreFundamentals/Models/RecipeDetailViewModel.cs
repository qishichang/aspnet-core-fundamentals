using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Models
{
    public class RecipeDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Method { get; set; }
        public IEnumerable<Item> Ingredients { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public bool CanEditRecipe { get; set; }

        public class Item
        {
            public string Name { get; set; }
            public string Quantity { get; set; }
        }
    }
}
