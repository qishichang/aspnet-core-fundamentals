using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCoreFundamentals.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreFundamentals.API
{
    [EnableCors("AllowShoppingApp")]
    [Route("api/[controller]", Name = "[controller]_[action]")]
    public class CarController : Controller
    {
        [EnableCors("AllowAnyOrigin")]
        [Route("[action]")]
        [Route("ignition")]
        [Route("/start-car")]
        public IActionResult Start()
        {
            return Ok("The car is starting.");
        }

        [Route("speed/{speed:int=20}")]
        [Route("/set-speed/{speed:int=20}", Name = "set_speed")]
        public IActionResult SetCarSpeed(int speed)
        {
            return Ok($"The speed of the car is {speed}.");
        }

        [DisableCors]
        [HttpPost]
        public IActionResult Add([FromBody]Car car)
        {
            return Ok(car);
        }
    }
}