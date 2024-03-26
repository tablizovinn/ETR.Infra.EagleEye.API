using ETR.Infra.EagleEye.API.Entity;
using ETR.Infra.EagleEye.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETR.Infra.EagleEye.API.Controllers
{
    [Route("api/v1/pulse")]
    [ApiController]
    public class PulseController : Controller
    {
        private readonly PulseDBContext _context;

        public PulseController(PulseDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult createPulse(string computer, string taskid, string status)
        {
            var newPulseLogs = new PulseLog
            {
                ComputerName = computer,
                Date = DateTime.Now,
                TaskID = taskid,
                Status = status
            };


            _context.PulseLogs.Add(newPulseLogs);
            _context.SaveChanges();
            return Ok(newPulseLogs);
        }
      
    }
}