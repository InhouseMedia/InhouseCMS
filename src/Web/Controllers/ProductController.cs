namespace Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Localization;

	using Newtonsoft.Json;

	using System;
	using System.Linq;
	using System.Net;
	using System.Text.RegularExpressions;
	using System.Threading;
	using System.Threading.Tasks;

	using Library.Config;
	using Library.Models;
	using Library.Repositories;

	//Done in startup
	//[ServiceFilter(typeof(LocalizationActionFilter))]
	public class ProductController : Controller
	{
		private readonly SiteConfig _config;
		private readonly IStringLocalizer<ProductController> _localizer;

		private readonly IProductRepository _repository;

		public ProductController(IProductRepository repository, IStringLocalizer<ProductController> localizer, ConfigRepository config)
		{
			_localizer = localizer;
			_repository = repository;
			_config = config.GetConfig();
		}

		public async Task<IActionResult> Index(string id)
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.GetById(id);

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			ViewBag.MetaTitle = result.MetaTitle;
			ViewBag.MetaDescription = result.MetaDescription;
			ViewBag.MetaKeywords = result.MetaKeywords;

			return View(result);
		}

		public async Task<IActionResult> List()
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.List();

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			var firstResult = result.FirstOrDefault();

			ViewBag.MetaTitle = firstResult.MetaTitle;
			ViewBag.MetaDescription = firstResult.MetaDescription;
			ViewBag.MetaKeywords = firstResult.MetaKeywords;

			return View(result);
		}
	}
}