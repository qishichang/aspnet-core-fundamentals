using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Data;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreFundamentals.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = _context.Todo.ToList();
            return View(model);
        }

        public string Index2() => "Index2";
    }
}