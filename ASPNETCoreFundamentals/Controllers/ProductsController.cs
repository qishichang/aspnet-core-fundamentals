using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreFundamentals.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var vm = new ProductListViewModel();
            vm.Discount = new Discount
            {
                Start = DateTime.Now,
                End = DateTime.Today.AddDays(30),
                Rate = 0.75
            };
            vm.Products = new List<Product>
            {
                new Product
                {
                    ID = 101,
                    Name = "Book",
                    Price = 20
                },
                new Product
                {
                    ID = 102,
                    Name = "Bike",
                    Price = 30
                },
                new Product
                {
                    ID = 103,
                    Name = "Fireworks",
                    Price = 40
                }
            };
            return View("ProductList", vm);
        }

        public IActionResult Edit()
        {
            var product = new Product(901, "Rocket", 99.99);
            return View(product);
        }

        public IActionResult Show()
        {
            var products = new List<Product>();
            products.Add(new Product(101, "Apple", 19.99));
            products.Add(new Product(201, "Bike", 29.99));
            products.Add(new Product(301, "Couch", 39.99));
            return View(products);
        }
    }
}