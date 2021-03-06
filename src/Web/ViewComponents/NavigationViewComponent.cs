﻿namespace Web.ViewComponents
{
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	using Library.Models;
	using Library.Repositories;

	public class NavigationViewComponent : ViewComponent
	{
		private readonly INavigationRepository _repository;

		public NavigationViewComponent(INavigationRepository repository)
		{
			_repository = repository;
		}

		public async Task<IViewComponentResult> InvokeAsync(ArticleContent model)
		{
			if (!ModelState.IsValid)
				return View(); //return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.GetNavigation();

		if (result == null)
				return  View(); //new StatusCodeResult(204); // 204 No Content

			return View(result);
		}
	}
}