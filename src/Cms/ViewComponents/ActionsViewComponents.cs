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

	public class ActionsViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(ArticlePage model)
		{
			return View(model);
		}
	}
}