namespace Web.Models
{
    using Microsoft.Extensions.Options;
    //using Microsoft.Extensions.Options;
    using Web.Repositories;

    public class Config
    {
        private readonly ConfigObject _config;
        public Config(IOptions<ApiUrl> apiUrl)
        {
            //apiUrl url should be available through dependency injection
            //domainname should be available through HttpContext.Request.Host.Host
            var clientApi = new ApiRespository(apiUrl, domainName);
            var configResponse = clientApi.GetResultsSync("config");

            var items = configResponse.Content.ReadAsAsync<ConfigObject>();
            _config = items.Result;
        }

        public ConfigObject GetConfig()
        {
            return _config;
        }
    }
}