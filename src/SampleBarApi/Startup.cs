using FluentValidation.AspNetCore;
using GiG.Core.ApplicationMetrics.Extensions;
using GiG.Core.ApplicationMetrics.Prometheus.Extensions;
using GiG.Core.DistributedTracing.OpenTelemetry.Exporters.Zipkin.Extensions;
using GiG.Core.DistributedTracing.OpenTelemetry.Extensions;
using GiG.Core.HealthChecks.AspNetCore.Extensions;
using GiG.Core.HealthChecks.Extensions;
using GiG.Core.Hosting.AspNetCore.Extensions;
using GiG.Core.Hosting.Extensions;
using GiG.Core.MultiTenant.Activity.Extensions;
using GiG.Core.Validation.FluentValidation.Web.Extensions;
using GiG.Core.Web.Docs.Extensions;
using GiG.Core.Web.Hosting.Extensions;
using GiG.Core.Web.Versioning.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SampleBarApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Forwarded Headers
            services.ConfigureForwardedHeaders();

            // Info Management
            services.ConfigureInfoManagement(Configuration);

            // Health Checks
            services.ConfigureHealthChecks(Configuration)
                .AddHealthChecks();
           
            // Application Metrics
            services.ConfigureApplicationMetrics(Configuration);

            // Tracing
            services.AddTracing(x => x.RegisterZipkin(), Configuration);
           
            // Web Api
            services.ConfigureApiDocs(Configuration)
                .ConfigureApiBehaviorOptions()
                .AddApiExplorerVersioning()
                .AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseForwardedHeaders();
            app.UsePathBaseFromConfiguration();
            app.UseRouting();
            app.UseHttpApplicationMetrics();
            app.UseApiDocs();
            app.UseTenantIdMiddleware();
            app.UseFluentValidationMiddleware();
            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllers();
                endpoints.MapInfoManagement();
                endpoints.MapHealthChecks();
                endpoints.MapApplicationMetrics();
            });            
        }
    }
}