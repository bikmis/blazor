using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.WebAPI.Controllers
{
    public class TestController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return Ok("Employee resource server is running.");
        }
    }
}
