using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreFundamentals.API
{
    public class CarController : Controller
    {
        [Route("car/start")]
        [Route("car/ignition")]
        [Route("start-car")]
        public IActionResult Start()
        {
            return Ok("The car is starting.");
        }

        [Route("car/speed/{speed}")]
        [Route("set-speed/{speed}")]
        public IActionResult SetCarSpeed(int speed)
        {
            return Ok($"The speed of the car is {speed}.");
        }
    }
}