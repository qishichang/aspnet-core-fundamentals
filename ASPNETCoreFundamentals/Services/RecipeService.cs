using ASPNETCoreFundamentals.Data;
using ASPNETCoreFundamentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Services
{
    public class RecipeService
    {
        private readonly AppDbContext _context;

        public RecipeService(AppDbContext context)
        {
            _context = context;
        }

        public int CreateRecipe(CreateRecipeCommand cmd)
        {
            var recipe = new Recipe
            {
                Name = cmd.Name,
                TimeToCook = new TimeSpan(
                    cmd.TimeToCookHrs, cmd.TimeToCookMins, 0),
                Method = cmd.Method,
                IsVegetarian = cmd.IsVegetarian,
                IsVegan = cmd.IsVegan,
                Ingredients = cmd.Ingredients?.Select(i =>
                    new Ingredient
                    {
                        Name = i.Name,
                        Quantity = i.Quantity,
                        Unit = i.Unit
                    }).ToList()

            };

            _context.Add(recipe);
            _context.SaveChanges();

            return recipe.RecipeId;
        }

        public ICollection<RecipeSummaryViewModel> GetRecipes()
        {
            return _context.Recipes
                    .Where(r => !r.IsDeleted)
                    .Select(r => new RecipeSummaryViewModel
                    {
                        Id = r.RecipeId,
                        Name = r.Name,
                        TimeToCook = $"{r.TimeToCook.TotalMinutes}mins"
                    })
                    .ToList();
        }

        public RecipeDetailViewModel GetRecipeDetail(int id)
        {
            return _context.Recipes
                    .Where(r => r.RecipeId == id)
                    .Select(r => new RecipeDetailViewModel
                    {
                        Id = r.RecipeId,
                        Name = r.Name,
                        Method = r.Method,
                        Ingredients = r.Ingredients
                            .Select(item => new RecipeDetailViewModel.Item
                            {
                                Name = item.Name,
                                Quantity = $"{item.Quantity} {item.Unit}"
                            })
                    })
                    .SingleOrDefault();
        }
    }
}
