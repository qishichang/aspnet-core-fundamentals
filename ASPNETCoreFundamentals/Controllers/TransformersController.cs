using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreFundamentals.Controllers
{
    public class TransformersController : Controller
    {
        public IActionResult Index()
        {
            var roles = new List<Transformer>();
            roles.Add(new Transformer(1, "Optimus Prime", "Earth", "Blue"));
            roles.Add(new Transformer(2, "Bumblebee", "Earth", "Yellow"));
            roles.Add(new Transformer(3, "Starscream", "Cybertron", "Red"));
            roles.Add(new Transformer(4, "Soundwave", "Cybertron", "Purple"));
            return View(roles);
        }
    }
}