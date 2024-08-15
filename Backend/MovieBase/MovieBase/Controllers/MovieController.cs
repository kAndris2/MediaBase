using System.Diagnostics;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MovieBase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        [HttpGet("stream")]
        [EnableCors("AllowAll")]
        public IActionResult StreamVideo()
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry("Stream requested", EventLogEntryType.Information, 101, 1);
            }

            var filePath = "";
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var response = new FileStreamResult(fileStream, "video/mp4");
            response.EnableRangeProcessing = true;
            return response;
        }
    }
}