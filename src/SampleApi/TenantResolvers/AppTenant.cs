using System.Collections.Generic;

namespace SampleApi.TenantResolvers
{
    public class AppTenant : IAppTenant
    {
        public string Name { get; set; }
        public IEnumerable<string> Hostnames { get; set; }
    }

    public interface IAppTenant
    {
        string Name { get; set; }
        IEnumerable<string> Hostnames { get; set; }
    }
}