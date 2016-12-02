namespace Web.Filters
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

	using Library.Config;
	using Web.Models;

	public class LocalizationActionFilter : ActionFilterAttribute
    {
        // private readonly string _webApiUrl = ConfigurationManager.AppSettings["WebApiUrl"];
	    private readonly SiteConfig _config;
        private readonly ILogger _logger;
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        public LocalizationActionFilter(
			ILoggerFactory loggerFactory, 
			IOptions<RequestLocalizationOptions> options,
			Config config)
        {
            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            _logger = loggerFactory.CreateLogger(nameof(LocalizationActionFilter));
            _localizationOptions = options;
	        _config = config.GetConfig();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
			var culture = _config.Language.Locale.First();

            CultureInfo.CurrentCulture = new CultureInfo(culture);
            CultureInfo.CurrentUICulture = new CultureInfo(culture);

        }
    }
}