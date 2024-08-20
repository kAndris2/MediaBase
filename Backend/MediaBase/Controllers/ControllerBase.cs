using Microsoft.AspNetCore.Mvc;
using MediaBase.Interfaces;

namespace MediaBase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControllerBase<T> : Controller
    {
        protected readonly ILogger<ControllerBase> logger;
        protected readonly IRequestManager<T> requestManager;

        public ControllerBase(IRequestManager<T> requestManager, ILogger<ControllerBase> logger)
        {
            this.requestManager = requestManager;
            this.logger = logger;
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
                logger.LogError(ex.Message);
                return BadRequest("An error occured while querying medias!");
            }
        }
    }
}