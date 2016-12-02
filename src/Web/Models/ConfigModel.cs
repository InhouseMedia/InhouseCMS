namespace Web.Models
{
	using System.Net.Http;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.Options;

	using Library.Config;
	using Library.Models;
	using Web.Repositories;

	public class Config
	{
        private readonly SiteConfig _config;

		public Config(IOptions<Api> api, IHttpContextAccessor httpContextAccessor)
		{
			var connectionName = httpContextAccessor.HttpContext.Request.Host.Host ?? "";
			
			var clientApi = new ApiRespository(api.Value.Url, connectionName);
            var configResponse = clientApi.GetResultsSync("config");

            var items = configResponse.Content.ReadAsAsync<SiteConfig>();
            _config = items.Result;
        }

        public SiteConfig GetConfig()
        {
            return _config;
        }
    }
}