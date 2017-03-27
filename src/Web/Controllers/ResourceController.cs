// Dummy class to group shared resources
namespace Web.Controllers
{
	using System;
	using System.Globalization;
	using System.Linq;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Mvc.Filters;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;

	using Library.Config;
	using Library.Repositories;

	public class ResourceController
	{
		private readonly SiteConfig _config;

		public ResourceController(ConfigRepository config)
		{
			_config = config.GetConfig();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Change()
		{
			if (!ModelState.IsValid) return View();

			var body = new BodyBuilder();
			var culture = Request.Form["locale"];

			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
			);
			//return LocalRedirect(returnUrl);
			//return
		}
	}
}