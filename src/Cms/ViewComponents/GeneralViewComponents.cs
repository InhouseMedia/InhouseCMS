namespace Cms.ViewComponents
{
	using Cms.Controllers;

	using Library.Config;
	using Library.Models;
	using Library.Repositories;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Localization;

	using System.Collections.Generic;
	using System.Reflection;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;

	public class GeneralViewComponent : ViewComponent
	{

		private readonly SiteConfig _config;
		private readonly IStringLocalizer<ResourceController> _sharedLocalizer;

		public GeneralViewComponent(ConfigRepository config, IStringLocalizer<ResourceController> sharedLocalizer)
		{
			_config = config.GetConfig();
			_sharedLocalizer = sharedLocalizer;
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
				var name = Regex.Match(item.Name, @"\<(\w+)\>").Groups[1].Value;

				var boxObject = new BoxVisibility
				{
					Active = (x == active) ? "active" : "",
					Name = name,
					Key = x,
					Translation = _sharedLocalizer[name]
				};

				ViewBag.Visibility.Add(boxObject);
			}

			return View(model);
		}
	}
}