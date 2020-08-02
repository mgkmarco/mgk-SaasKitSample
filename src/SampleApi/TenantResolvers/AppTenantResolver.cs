using Microsoft.AspNetCore.Http;
using SaasKit.Multitenancy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApi.TenantResolvers
{
    public class AppTenantResolver : ITenantResolver<IAppTenant>
    {
        private readonly Dictionary<string, string> mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "localhost:5000", "Default"},
            { "localhost:5001", "Tenant_1"},
            { "localhost:5002", "Tenant_2"},
            { "localhost:5003", "Tenant_3"},
        };
        
        public Task<TenantContext<IAppTenant>> ResolveAsync(HttpContext context)
        {
            TenantContext<IAppTenant> tenantContext = null;

            if (mappings.TryGetValue(context.Request.Host.Value, out var tenantName))
            {
                tenantContext = new TenantContext<IAppTenant>(
                    new AppTenant { Name = tenantName, Hostnames = new[] { context.Request.Host.Value } });

                tenantContext.Properties.Add("Created", DateTime.UtcNow);
            }

            return Task.FromResult(tenantContext);
        }
    }
}