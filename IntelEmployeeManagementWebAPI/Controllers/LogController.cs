using Intel.EmployeeManagement.Data;
using Intel.EmployeeManagement.Data.Entities;
using Intel.EmployeeManagement.WebAPI.Models.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Intel.EmployeeManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class LogController : Controller
    {
        private EmployeeDbContext employeeDbContext { get; set; }

        public LogController(EmployeeDbContext _employeeDbContext)
        {
            employeeDbContext = _employeeDbContext;
        }

        [HttpPost]
        [Route("log")]
        public IActionResult Log(LogRequest request)
        {
            var log = new Log() {
                LogLevel = request.LogLevel,
                EventName = request.EventName,
                CreateDate = request.CreateDate,
                ExceptionMessage = request.ExceptionMessage,
                Source = request.Source,
                StackTrace = request.StackTrace
            };

            employeeDbContext.Logs.Add(log);
            employeeDbContext.SaveChanges();
            return Ok();
        }
    }
}
