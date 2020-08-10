using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;
using SaasKit.Multitenancy;
using SampleApi.Clients;
using SampleApi.TenantResolvers;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SampleApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMultiTenancy(this IServiceCollection services)
        {
            services.AddMultitenancy<IAppTenant, AppTenantResolver>();
        }

        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddScoped<Func<string, ITenantClient>>(provider => tenantIdentifier =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(tenantIdentifier);
                
                switch(tenantIdentifier)
                {
                    case "foo":
                    {
                        httpClient.BaseAddress = new Uri(configuration["TenantApis:Foo"]);
                        System.Diagnostics.Debug.WriteLine(configuration["TenantApis:Foo"]);
                        Console.WriteLine(configuration["TenantApis:Foo"]);
                        httpClient.DefaultRequestHeaders.Add("x-api-key", "this-is-the-value-of-x-api-key");
                        break;
                    }

                    case "bar":
                    {
                        httpClient.BaseAddress = new Uri(configuration["TenantApis:Bar"]);
                        httpClient.DefaultRequestHeaders.Add("x-api-key", "this-is-the-value-of-x-api-key");
                        break;
                    }

                    default:
                        throw new KeyNotFoundException($"No http client implementation for the given tenant!");
                }
                
                return RestService.For<ITenantClient>(httpClient);   
            });
        }
    }
}