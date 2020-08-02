using Microsoft.AspNetCore.Mvc;
using SampleApi.TenantResolvers;
using System;

namespace SampleApi.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class HelloWorldController : ControllerBase
    {
        private readonly IAppTenant _appTenant;
        public HelloWorldController(IAppTenant appTenant)
        {
            _appTenant = appTenant ?? throw new ArgumentNullException(nameof(appTenant));
        }
        
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok($"{_appTenant.Name} here motherfucker!");  
        }
    }
}