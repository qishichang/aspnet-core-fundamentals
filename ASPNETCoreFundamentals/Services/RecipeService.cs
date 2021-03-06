﻿using ASPNETCoreFundamentals.Data;
using ASPNETCoreFundamentals.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.Services
{
    public class RecipeService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _log;

        public RecipeService(AppDbContext context, ILoggerFactory factory)
        {
            _context = context;
            _log = factory.CreateLogger("RecipeApp.RecipeService");
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

        public ICollection<RecipeSummaryViewModel> GetRecipesForUser(string userId, int numberOfRecipes)
        {
            return _context.Recipes
                    .Where(r => r.CreatedById == userId)
                    .Where(r => !r.IsDeleted)
                    .OrderBy(r => r.LastModified)
                    .Take(numberOfRecipes)
                    .Select(r => new RecipeSummaryViewModel
                    {
                        Id = r.RecipeId,
                        Name = r.Name
                    }).ToList();
        }

        public bool DoesRecipeExist(int id)
        {
            return _context.Recipes
                    .Where(r => !r.IsDeleted)
                    .Where(r => r.RecipeId == id)
                    .Any();
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
                        LastModified = r.LastModified,
                        Ingredients = r.Ingredients
                            .Select(item => new RecipeDetailViewModel.Item
                            {
                                Name = item.Name,
                                Quantity = $"{item.Quantity} {item.Unit}"
                            })
                    })
                    .SingleOrDefault();
        }

        public Recipe GetRecipe(int id)
        {
            return _context.Recipes
                    .Find(id);
        }

        public void UpdateRecipe(UpdateRecipeCommand cmd)
        {
            var recipe = _context.Recipes.Find(cmd.Id);
            if (recipe == null)
            {
                throw new Exception("Unable to find the recipe");
            }
            UpdateRecipe(recipe, cmd);
            _context.SaveChanges();
        }

        public void DeleteRecipe(int recipeId)
        {
            var recipe = _context.Recipes.Find(recipeId);
            if (recipe == null)
            {
                throw new Exception("Unable to find the recipe");
            }
            recipe.IsDeleted = true;
            _context.SaveChanges();
        }

        private void UpdateRecipe(Recipe recipe, UpdateRecipeCommand cmd)
        {
            recipe.Name = cmd.Name;
            recipe.TimeToCook = new TimeSpan(cmd.TimeToCookHrs, cmd.TimeToCookMins, 0);
            recipe.Method = cmd.Method;
            recipe.IsVegetarian = cmd.IsVegetarian;
            recipe.IsVegan = cmd.IsVegan;
            recipe.LastModified = DateTimeOffset.UtcNow;
        }

        public IList<Recipe> FindRecipe(string search)
        {
            return _context.Recipes
                    .FromSql("SELECT * FROM Recipes WHERE Name = '{0}'", search)
                    .ToList();
        }
    }
}
