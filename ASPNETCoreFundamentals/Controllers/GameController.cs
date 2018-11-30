using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.ModelBinders;
using ASPNETCoreFundamentals.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreFundamentals.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([ModelBinder(BinderType = typeof(GameModelBinder))] Game game)
        {
            // business logic
            return View(game);
        }

        public IActionResult Edit()
        {
            Game game = new Game
            {
                City = "Seattle",
                Player1 = new Player
                {
                    Name = "Superman",
                    Rank = 101
                },
                Player2 = new Player
                {
                    Name = "IronMan",
                    Rank = 202
                },
            };
            return View(game);
        }

        [HttpPost]
        public IActionResult Edit(Game game)
        {
            return View(game);
        }
    }
}