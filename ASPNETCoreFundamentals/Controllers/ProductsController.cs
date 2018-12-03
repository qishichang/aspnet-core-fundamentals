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
    }
}