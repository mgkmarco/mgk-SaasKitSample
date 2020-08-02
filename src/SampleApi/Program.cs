using GiG.Core.Configuration.Extensions;
using GiG.Core.DistributedTracing.Activity.Extensions;
using GiG.Core.Hosting.Extensions;
using GiG.Core.Logging.All.Extensions;
using GiG.Core.MultiTenant.Activity.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SampleApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .UseApplicationMetadata()
                .ConfigureServices(x =>
                {
                    x.AddActivityContextAccessor();
                    x.AddActivityTenantAccessor();
                })
                .ConfigureExternalConfiguration()
                .ConfigureLogging()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:5000", "http://localhost:5001", "http://localhost:5002",
                        "http://localhost:5003");
                });
    }
}