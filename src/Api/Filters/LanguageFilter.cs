namespace Api.Filters
{
	using System;
	using System.Globalization;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Mvc.Filters;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;

	public sealed class LanguageActionFilter : ActionFilterAttribute
	{
		private readonly ILogger _logger;
		private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

		public LanguageActionFilter(ILoggerFactory loggerFactory, IOptions<RequestLocalizationOptions> options)
		{
			if (loggerFactory == null)
				throw new ArgumentNullException(nameof(loggerFactory));

			if (options == null)
				throw new ArgumentNullException(nameof(options));

			_logger = loggerFactory.CreateLogger(nameof(LanguageActionFilter));
			_localizationOptions = options;
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var culture = context.RouteData.Values["culture"]?.ToString() ?? "en-US";
			
			switch (culture)
			{
				case "nl":
					culture = "nl-NL";
					break;
				case "en":
					culture = "en-US";
					break;
				default:
					break;
			}

			_logger.LogInformation($"Setting the culture from the URL: {culture}");

			CultureInfo.CurrentCulture = new CultureInfo(culture);
			CultureInfo.CurrentUICulture = new CultureInfo(culture);

			base.OnActionExecuting(context);
		}
	}
}