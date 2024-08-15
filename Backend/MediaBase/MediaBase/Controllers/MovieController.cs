using System.Diagnostics;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MediaBase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        [HttpGet("stream")]
        [EnableCors("AllowAll")]
        public IActionResult StreamVideo()
        {
            var filePath = "";
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var response = new FileStreamResult(fileStream, "video/mp4");
            response.EnableRangeProcessing = true;
            return response;
        }
    }
}