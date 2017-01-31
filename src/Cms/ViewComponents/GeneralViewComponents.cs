namespace Cms.ViewComponents
{
	using Library.Config;
	using Library.Models;
	using Library.Repositories;

	using Microsoft.AspNetCore.Mvc;

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Reflection;

	public class GeneralViewComponent : ViewComponent
	{

		private readonly SiteConfig _config;

		public GeneralViewComponent(ConfigRepository config)
		{
			_config = config.GetConfig();
		}

		public async Task<IViewComponentResult> InvokeAsync(ArticlePage model)
		{
			// ConfigClass for all visibility options
			var se = _config.SearchEngines;
			var t = typeof(Visibility);
			var properties = t.GetTypeInfo().DeclaredFields;

			ViewBag.Visibility = new List<BoxVisibility>();
			//ViewBag.Templates = new List<SelectListItem>();

			// Setup visibilty list
			foreach (var item in properties)
			{
				// Get current visibility value from the ConfigClass
				var x = (int)item.GetValue(se.Visibility);
				// TODO: Active should be changed in the database from boolean to int
				var active = model.Active ? 1 : 0;

				var boxObject = new BoxVisibility
				{
					Active = (x == active) ? "active" : "",
					Name = item.Name,
					Key = x,
					Translation = "" //manager.GetString(item.Name)
				};

				ViewBag.Visibility.Add(boxObject);
			}

			return View(model);
		}
	}
}