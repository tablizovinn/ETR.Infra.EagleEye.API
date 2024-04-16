using ETR.Infra.EagleEye.API.Entity;
using ETR.Infra.EagleEye.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using ETR.Infra.EagleEye.API.Entity.ETR.Infra.EagleEye.API.Models;
using System.Linq;

namespace ETR.Infra.EagleEye.API.Controllers
{
    [Route("api/v1/pulse")]
    [ApiController]
    public class PulseController : ControllerBase
    {
        private readonly PulseDBContext _context;

        public PulseController(PulseDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateOrUpdatePulse([FromBody] PulseRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request body.");
            }

            var existingPulseLogs = _context.PulseLogs.Where(p => p.TaskID == request.TaskId).ToList();

            if (existingPulseLogs.Count == 0)
            {
                // Create new pulse log entries for "Starting", "Uploading", and "Finished"
                var startingPulseLog = new PulseLog
                {
                    ComputerName = request.Computer,
                    Date = DateTime.Now,
                    TaskID = request.TaskId,
                    Status = "Starting"
                };
                var uploadingPulseLog = new PulseLog
                {
                    ComputerName = request.Computer,
                    Date = DateTime.Now,
                    TaskID = request.TaskId,
                    Status = "Uploading"
                };
                var finishedPulseLog = new PulseLog
                {
                    ComputerName = request.Computer,
                    Date = DateTime.Now,
                    TaskID = request.TaskId,
                    Status = "Finished"
                };

                _context.PulseLogs.AddRange(startingPulseLog, uploadingPulseLog, finishedPulseLog);
            }
            else
            {
                // Ensure "Starting" status is retained, update only "Uploading" status
                var startingStatusExists = existingPulseLogs.Any(p => p.Status == "Starting");
                if (!startingStatusExists)
                {
                    var existingUploadingLog = existingPulseLogs.FirstOrDefault(p => p.Status == "Uploading");
                    if (existingUploadingLog != null)
                    {
                        existingUploadingLog.Status = "Uploading"; // Ensure it's still uploading
                        existingUploadingLog.Date = DateTime.Now; // Optionally update the date
                        _context.PulseLogs.Update(existingUploadingLog);
                    }
                }
            }

            _context.SaveChanges();

            return Ok("Pulse logs created or updated successfully.");
        }
    }
}
