using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SaasKit.Multitenancy;
using SampleApi.Clients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApi.TenantResolvers
{
    public class AppTenantResolver : MemoryCacheTenantResolver<IAppTenant> 
    {
        private const string TenantHeaderIdentifier = "X-Tenant-ID";
        private readonly Func<string, ITenantClient> _tenantClientFactory;

        public AppTenantResolver(IMemoryCache cache, ILoggerFactory loggerFactory,
            Func<string, ITenantClient> tenantClientFactory) : base(cache, loggerFactory)
        {
            _tenantClientFactory = tenantClientFactory ?? throw new ArgumentNullException(nameof(tenantClientFactory));
        }

        protected override string GetContextIdentifier(HttpContext context)
        {
            return context.Request.Headers[TenantHeaderIdentifier].ToString().ToLower();
        }

        protected override IEnumerable<string> GetTenantIdentifiers(TenantContext<IAppTenant> context)
        {
            return new[] {context.Tenant.Name};
        }
        
        /// <summary>
        /// This is an optional method to override the default caching of the base class which is that of 1hr.
        /// https://benfoster.io/blog/aspnet-core-multi-tenancy-tenant-lifetime/
        /// </summary>
        /// <returns></returns>
        protected override MemoryCacheEntryOptions CreateCacheEntryOptions()
        {
            return new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(new TimeSpan(0, 30, 0)); 
        }

        protected override Task<TenantContext<IAppTenant>> ResolveAsync(HttpContext context)
        {
            TenantContext<IAppTenant> tenantContext = new TenantContext<IAppTenant>(new AppTenant()
            {
                Name    = "Default",
                Hostnames = new string [0]
            });
            
            var tenantName = context.Request.Headers[TenantHeaderIdentifier];

            if (!string.IsNullOrWhiteSpace(tenantName))
            {
                var client = _tenantClientFactory(tenantName);
                tenantContext = new TenantContext<IAppTenant>(new AppTenant()
                {
                    Name = tenantName,
                    Hostnames = new[] { context.Request.Host.Value },
                    Client = client 
                });   
            }

            return Task.FromResult(tenantContext);
        }
    }
}