using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.IdentityProvider.Controllers
{
    [ApiController]
    [Route("")]
    public class HealthCheckController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Identity provider service is running.");
        }
    }
}
