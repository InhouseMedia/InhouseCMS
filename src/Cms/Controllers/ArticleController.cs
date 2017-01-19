using Library.Config;

namespace Cms.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Localization;

	using System.Threading.Tasks;

	using Web.Repositories;

	public class ArticleController: Controller
	{
		private readonly SiteConfig _config;
		private readonly IStringLocalizer<Web.Controllers.ArticleController> _localizer;

		private readonly IArticleRepository _repository;

		public ArticleController(IArticleRepository repository, IStringLocalizer<Web.Controllers.ArticleController> localizer, ConfigRepository config)
		{
			_localizer = localizer;
			_repository = repository;
			_config = config.GetConfig();
		}

		public async Task<IActionResult> Index(string id)
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.GetPage(id);

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			ViewBag.boxes = _config.Controllers.Article.Tools;

			return View(result);
		}
	}
}