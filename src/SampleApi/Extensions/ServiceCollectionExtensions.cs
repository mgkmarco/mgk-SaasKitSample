using Microsoft.Extensions.DependencyInjection;
using SampleApi.TenantResolvers;

namespace SampleApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMultiTenancy(this IServiceCollection services)
        {
            services.AddMultitenancy<IAppTenant, AppTenantResolver>();
        }
    }
}