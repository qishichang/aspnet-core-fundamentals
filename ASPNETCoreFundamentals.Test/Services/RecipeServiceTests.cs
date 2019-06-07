using ASPNETCoreFundamentals.Data;
using ASPNETCoreFundamentals.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ASPNETCoreFundamentals.Test.Services
{
    public class RecipeServiceTests
    {
        [Fact]
        public void GetRecipeDetail_CanLoadFromContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureCreated();

                context.Recipes.AddRange(
                    new Models.Recipe { RecipeId = 1, Name = "Recipe1" },
                    new Models.Recipe { RecipeId = 2, Name = "Recipe2" },
                    new Models.Recipe { RecipeId = 3, Name = "Recipe3" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                Mock<ILoggerFactory> factory = new Mock<ILoggerFactory>();
                Mock<ILogger> logger = new Mock<ILogger>();
                factory.Setup(f => f.CreateLogger("RecipeApp.RecipeService")).Returns(logger.Object);

                var service = new RecipeService(context, factory.Object);

                var recipe = service.GetRecipeDetail(id: 2);

                recipe.ShouldNotBeNull();
                recipe.Id.ShouldBe(2);
                recipe.Name.ShouldBe("Recipe2");
            }



        }
    }
}
