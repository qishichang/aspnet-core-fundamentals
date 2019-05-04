using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Models;

namespace ASPNETCoreFundamentals.Services
{
    public class CreateRecipeCommand
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Range(0, 24), DisplayName("Time to cook (hrs)")]
        public int TimeToCookHrs { get; set; }
        [Range(0, 59), DisplayName("Time to cook (mins)")]
        public int TimeToCookMins { get; set; }
        public string Method { get; set; }
        [DisplayName("Vegetarian?")]
        public bool IsVegetarian { get; set; }
        [DisplayName("Vegan?")]
        public bool IsVegan { get; set; }
        public ICollection<CreateIngredientCommand> Ingredients { get; internal set; }
    }
}
