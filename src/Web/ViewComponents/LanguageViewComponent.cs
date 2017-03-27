namespace Web.ViewComponents
{
	using Library.Config;
	using Library.Models;
	using Library.Repositories;

	using Microsoft.AspNetCore.Mvc;

	public class LanguageViewComponent : ViewComponent
	{
		private readonly SiteConfig _config;

		public LanguageViewComponent(ConfigRepository config)
		{
			_config = config.GetConfig();
		}

		public IViewComponentResult Invoke()
		{
			return View(_config.Language.Locale);
		}
	}
}