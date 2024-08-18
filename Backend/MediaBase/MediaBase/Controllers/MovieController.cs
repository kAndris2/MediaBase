using Microsoft.AspNetCore.Mvc;
using MediaBase.Services;

namespace MediaBase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly RequestManager _requestManager;

        public MovieController(RequestManager requestManager)
        {
            _requestManager = requestManager;
        }

        [HttpGet("[action]")]
        public IActionResult GetMovies()
        {
            try
            {
                var movies = _requestManager.GetMovies();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("stream")]
        public IActionResult GetStream([FromQuery] string movieName)
        {
            try
            {
                var filePath = "";
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var response = new FileStreamResult(fileStream, "video/mp4");
                response.EnableRangeProcessing = true;
                return response;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}