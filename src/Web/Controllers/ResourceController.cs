// Dummy class to group shared resources
namespace Web.Controllers
{
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Localization;
	using Microsoft.AspNetCore.Mvc;

	using MimeKit;

	using Org.BouncyCastle.Asn1.Ocsp;

	using System;
	using System.Threading.Tasks;

	using Library.Config;
	using Library.Repositories;

	public class ResourceController : Controller
	{
		private readonly SiteConfig _config;

		public ResourceController(ConfigRepository config)
		{
			_config = config.GetConfig();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Change()
		{
			if (!ModelState.IsValid) return Redirect("/");

			var culture = Request.Form["locale"];

			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
			);

			return Redirect(Request.HttpContext.Request.Host.Host);
		}
	}
}