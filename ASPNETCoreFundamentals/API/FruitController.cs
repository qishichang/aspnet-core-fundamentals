using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreFundamentals.API
{
    public class FruitController : Controller
    {
        List<string> _fruits = new List<string>
        {
            "Pear",
            "Lemon",
            "Peach"
        };

        public IEnumerable<string> Index()
        {
            return _fruits;
        }
    }
}
