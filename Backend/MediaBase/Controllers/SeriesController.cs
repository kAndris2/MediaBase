using Microsoft.AspNetCore.Mvc;
using MediaBase.Models;
using MediaBase.Services;

namespace MediaBase.Controllers
{
    public class SeriesController : ControllerBase<SeriesEpisode>
    {
        public SeriesController(SeriesRequestManager requestManager, ILogger<SeriesController> logger)
            : base(requestManager, logger) { }

        [HttpGet("stream")]
        public IActionResult GetStream([FromQuery] string title, [FromQuery] int season, [FromQuery] int episode)
        {
            try
            {
                var fileStreamResult = ((SeriesRequestManager)requestManager).GetStream(title, season, episode);
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