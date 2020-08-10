using Microsoft.AspNetCore.Mvc;

namespace SampleBarApi.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class HelloworldController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => Ok("Hello from Bar tenant!");
    }
}