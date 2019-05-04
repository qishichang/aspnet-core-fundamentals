using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreFundamentals.Services
{
    public class CreateIngredientCommand
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Range(0, int.MaxValue)]
        public decimal Quantity { get; set; }
        [StringLength(20)]
        public string Unit { get; set; }
    }
}