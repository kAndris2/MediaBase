using Microsoft.AspNetCore.Mvc;
using MediaBase.Models;
using MediaBase.Services;

namespace MediaBase.Controllers
{
    public class MovieController : ControllerBase<Movie>
    {
        public MovieController(MovieRequestManager requestManager, ILogger<MovieController> logger)
            : base(requestManager, logger) { }

        [HttpGet("stream")]
        public IActionResult GetStream([FromQuery] string title, [FromQuery] int year)
        {
            try
            {
                var fileStreamResult = ((MovieRequestManager)requestManager).GetStream(title, year);
                return fileStreamResult;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest("An error occurred while trying to get the stream!");
            }
        }
    }
}