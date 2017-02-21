namespace Cms.ViewComponents
{
	using Library.Config;
	using Library.Models;
	using Library.Repositories;

	using Microsoft.AspNetCore.Mvc;

	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class ActionsViewComponent : ViewComponent
	{
		private readonly SiteConfig _config;

		public ActionsViewComponent(ConfigRepository config)
		{
			_config = config.GetConfig();
		}

		public async Task<IViewComponentResult> InvokeAsync(ArticlePage model)
		{
			var actions = _config.Controllers.Article.Actions;

			var items = new List<NavigationSitemap>();

			foreach(var item in actions){
				var nav = new NavigationSitemap();
				nav.Title = item;

				items.Add(nav);
			}


			return View(items);
		}
	}
}