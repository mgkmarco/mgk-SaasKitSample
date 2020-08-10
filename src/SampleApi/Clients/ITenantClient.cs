using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace SampleApi.Clients
{
    public interface ITenantClient
    {
        [Get("/HelloWorld")]
        Task<HttpResponseMessage> GetAsync();
    }
}