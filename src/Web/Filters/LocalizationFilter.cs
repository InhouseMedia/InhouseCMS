namespace Web.Filters
{
    using System;
    using System.Globalization;
    //using System.Threading;
    // using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Routing;

    public class LocalizationActionFilter : ActionFilterAttribute
    {
        // private readonly string _webApiUrl = ConfigurationManager.AppSettings["WebApiUrl"];
        private readonly ILogger _logger;
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        public LocalizationActionFilter(ILoggerFactory loggerFactory, IOptions<RequestLocalizationOptions> options, IOptions<RouteOptions> routeOptions)
        {
            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            _logger = loggerFactory.CreateLogger(nameof(LocalizationActionFilter));
            _localizationOptions = options;

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var siteName = filterContext.HttpContext.Request.Host.Host ?? "";

            //GetConfig(siteName);

            //var cultureInfo = new CultureInfo(MvcApplication.Config.Language.Locale.First());
            var culture = "nl-NL";

            CultureInfo.CurrentCulture = new CultureInfo(culture);
            CultureInfo.CurrentUICulture = new CultureInfo(culture);

        }
        /*
                private void GetConfig(string domainName)
                {
                    if (MvcApplication.Config != null &&
                        ClientApiClass.GetConnectionString(domainName) == MvcApplication.Config.Domain) return;

                    // Get Config
                    var clientApi = new ClientApiClass(_webApiUrl, domainName);
                    var configResponse = clientApi.GetResultsSync("config");

                    var items = configResponse.Content.ReadAsAsync<ConfigObject>();
                    MvcApplication.Config = items.Result;
                }*/
    }
}