using Intel.EmployeeManagement.Data.Entities;
using Intel.EmployeeManagement.WebAPI.Models.Logging;
using Intel.EmployeeManagement.WebAPI.Services.Database_Service;
using Microsoft.AspNetCore.Mvc;

namespace Intel.EmployeeManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class LogController : Controller
    {
        private IDatabaseService databaseService { get; set; }

        public LogController(IDatabaseService _databaseService)
        {
            databaseService = _databaseService;
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

            databaseService.EmployeeDbContext.Logs.Add(log);
            databaseService.EmployeeDbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("dividebyzero")]
        public IActionResult DivideByZero() {
            var zero = 0;
            return Ok(1/zero);
        }
    }
}
