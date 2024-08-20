using Microsoft.AspNetCore.Mvc;
using MediaBase.Interfaces;

namespace MediaBase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControllerBase<T> : Controller
    {
        protected readonly IRequestManager<T> requestManager;

        public ControllerBase(IRequestManager<T> requestManager)
        {
            this.requestManager = requestManager;
        }

        [HttpGet("[action]")]
        public IActionResult GetTitles()
        {
            try
            {
                var medias = requestManager.GetTitles();
                return Ok(medias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}