namespace Web.Repositories
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class ApiRespository
    {
        private const string ConnectionString = "Inhouse";
	    private const string DefaultApiServer = "Http://localhost:5000";
        private readonly string _newConnectionString;
        private readonly string _apiserver;

        public ApiRespository(string url, string domain)
        {
            _newConnectionString = GetConnectionString(domain);
            //_newConnectionString += "Connection";
            _apiserver = url != "" ? url : DefaultApiServer;
        }

        public static string GetConnectionString(string domain)
        {
            var domainList = domain.Split('.');
            var isLocalhost = domainList.Any(x => x.Equals("localhost"));
            var isBinckDna = domainList.Any(x => x.Equals("binckdna"));
            if (domainList.Any() && !isLocalhost && domain != "")
            {
                domain = (isBinckDna) ? domainList.First() : domainList[1];
            }
            else
            {
                domain = ConnectionString;
            }
            return domain;
        }
        public async Task<HttpResponseMessage> GetResults(string call)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiserver);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("ConnectionKey", _newConnectionString);
                return await client.GetAsync(call);
            }
        }

        public HttpResponseMessage GetResultsSync(string call)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiserver);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("ConnectionKey", _newConnectionString);
                return client.GetAsync(call).Result;
            }
        }
    }
}