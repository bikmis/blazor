using Microsoft.AspNetCore.Mvc;
using System;

namespace Intel.EmployeeManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ExceptionExampleController : Controller
    {
        [HttpGet]
        [Route("dividebyzero")]
        public IActionResult DivideByZero()
        {
            try
            {
                var zero = 0;
                return Ok(1 / zero);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
