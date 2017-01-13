namespace Cms.Repositories
{
    using Cms.Connections;

    using Library.Config;

    using System.Net.Http;

    public class ConfigRepository
    {
        private readonly ApiConnection _api;
        private readonly SiteConfig _config;

        public ConfigRepository(ApiConnection api)
        {
            _api = api;
            var conn = _api.Connect("config");
            var temp = conn.Content.ReadAsAsync<SiteConfig>();
            _config = temp.Result;
        }

        public SiteConfig GetConfig()
        {
            return _config;
        }
    }
}