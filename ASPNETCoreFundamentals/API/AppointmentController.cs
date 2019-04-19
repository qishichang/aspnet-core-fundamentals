using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreFundamentals.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        [HttpGet("/appointments")]
        public IActionResult ListAppointments()
        {
            return Ok("Appointments List");
        }

        [HttpPost("/appointments")]
        public IActionResult CreateAppointment()
        {
            return Ok("Appointment created successfully");
        }
    }
}