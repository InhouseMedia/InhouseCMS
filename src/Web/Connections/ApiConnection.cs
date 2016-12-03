namespace Web.Connections
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Library.Models;
    using System.Linq;

    public class ApiConnection
    {
        private readonly Api _api;
        private readonly string _apiConnection;
        private readonly string _databaseName;

        public ApiConnection(IOptions<Api> api, IHttpContextAccessor httpContextAccessor)
        {
            _api = api.Value;

            //TODO: Get api url from hostname, otherwise fallback (connection.ApiConnection) (also when = localhost)
            //_apiConnection = httpContextAccessor.HttpContext.Request.Host.ToString() ?? connection.ApiConnection;
            //_databaseName = httpContextAccessor.HttpContext.Request.Host.Host ?? connection.DatabaseName;
            _apiConnection = _api.ApiConnection;
            _databaseName = httpContextAccessor.HttpContext.Request.Host.Host;

            var domainList = _databaseName.Split('.');
            var isLocalhost = domainList.Any(x => x.Equals("localhost"));
            var isBinckDna = domainList.Any(x => x.Equals("binckdna"));
            if (domainList.Any() && !isLocalhost && _databaseName != "")
            {
                _databaseName = (isBinckDna) ? domainList.First() : domainList[1];
            }
            else
            {
                _databaseName = _api.DatabaseName;
            }
        }

        public async Task<HttpResponseMessage> ConnectAsync(string call)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiConnection);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("ConnectionKey", _databaseName);
                return await client.GetAsync(call);
            }
        }

        public HttpResponseMessage Connect(string call)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiConnection);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("ConnectionKey", _databaseName);
                return client.GetAsync(call).Result;
            }
        }
    }
}