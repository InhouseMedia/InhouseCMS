namespace Cms.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Localization;

	using System.Threading.Tasks;

	using Web.Repositories;
	
	[Area("Cms")]
	public class ArticleController : Web.Controllers.ArticleController
	{
		public ArticleController(IArticleRepository repository, IStringLocalizer<Web.Controllers.ArticleController> localizer, ConfigRepository config) : base(repository, localizer, config)
		{
		}

		public override async Task<IActionResult> Index(string id)
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.GetPage(id);

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			ViewBag.boxes = _config.Controllers.Article.Tools;

			return View("~/Views/Article/Index.cshtml", result);
		}
	}
}