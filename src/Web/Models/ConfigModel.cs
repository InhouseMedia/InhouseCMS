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
		private readonly Api _api;
        private readonly SiteConfig _config;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public Config(IOptions<Api> api, IOptions<HttpContextAccessor> httpContextAccessor)
		{
			_api = api.Value;
			_httpContextAccessor = httpContextAccessor.Value;

			//var connectionName = _httpContextAccessor.HttpContext.Request.Host.Host ?? "";
			var connectionName = "";

			var clientApi = new ApiRespository(_api.Url, connectionName);
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