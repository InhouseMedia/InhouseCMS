namespace Web.Filters {
	using System;
	using System.Globalization;
	using System.Linq;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Mvc.Filters;
	using Microsoft.AspNetCore.Localization;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;

	using Library.Config;
	using Library.Repositories;

	public class LocalizationActionFilter: ActionFilterAttribute {
		private readonly SiteConfig _config;
		private readonly RequestLocalizationOptions _localizationOptions;
		private readonly ILogger _logger;

		public LocalizationActionFilter(
			ILoggerFactory loggerFactory,
			IOptions <RequestLocalizationOptions> options,
			ConfigRepository config) {
			if (loggerFactory == null)
				throw new ArgumentNullException(nameof(loggerFactory));

			if (options == null)
				throw new ArgumentNullException(nameof(options));

			_logger = loggerFactory.CreateLogger(nameof(LocalizationActionFilter));
			_localizationOptions = options.Value;
			_config = config.GetConfig();
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext) {
			var cookie = filterContext.HttpContext.Request.Cookies["locale"]; //context.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

			var tmpCulture = cookie ?? _config.Language.Locale.FirstOrDefault();

			var cultures = CookieRequestCultureProvider.ParseCookieValue(tmpCulture);

			var culture = cultures.Cultures.FirstOrDefault();
			var uICulture = cultures.UICultures.FirstOrDefault();

			CultureInfo.CurrentCulture = new CultureInfo(culture);
			CultureInfo.CurrentUICulture = new CultureInfo(uICulture);

			CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
			CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(uICulture);
		}
	}
}