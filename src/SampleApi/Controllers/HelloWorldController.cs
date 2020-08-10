using Microsoft.AspNetCore.Mvc;
using SampleApi.TenantResolvers;
using System;
using System.Threading.Tasks;

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
        public async Task<ActionResult<string>> GetAsync()
        {
            var responseMessage = $"Hello from {_appTenant.Name} tenant!";

            if (_appTenant.Client != null)
            {
                var tenantApiResponse =await _appTenant.Client.GetAsync();
                var message = await tenantApiResponse.Content.ReadAsStringAsync();
            }

            return Ok($"This is the response: {responseMessage}");  
        }
    }
}