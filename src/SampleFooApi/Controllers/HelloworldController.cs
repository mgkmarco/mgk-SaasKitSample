using Microsoft.AspNetCore.Mvc;

namespace SampleFooApi.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class HelloworldController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => Ok("Hello from Foo tenant!");
    }
}