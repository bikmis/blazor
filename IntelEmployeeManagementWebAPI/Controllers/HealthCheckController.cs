using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("")]
    public class HealthCheckController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return Ok("Employee resource service is running.");
        }
    }
}
