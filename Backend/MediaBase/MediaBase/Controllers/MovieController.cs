using Microsoft.AspNetCore.Mvc;
using MediaBase.Models;
using MediaBase.Services;

namespace MediaBase.Controllers
{
    public class MovieController : ControllerBase<Movie>
    {
        public MovieController(MovieRequestManager requestManager)
            : base(requestManager) { }

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
                return BadRequest(ex.Message);
            }
        }
    }
}