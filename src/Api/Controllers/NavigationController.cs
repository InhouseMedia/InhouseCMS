namespace Api.Controllers
{
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Localization;

	using MongoDB.Bson;

	using Api.Connections;
	using Api.Filters;
	using Api.Repositories;

	[ServiceFilter(typeof(LanguageActionFilter))]
	[Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/[controller]")]
	[Route("[controller]")]
	public class NavigationController : Controller
	{
		private readonly INavigationRepository _repository;
		private readonly IStringLocalizer<NavigationController> _localizer;

		public NavigationController(INavigationRepository repository, IStringLocalizer<NavigationController> localizer, IDatabaseConnection connection)
		{
			_repository = repository;
			_localizer = localizer;
		}

		[HttpGet]
		//[ValidateAntiForgeryToken]
		//[Authorize]
		public async Task<IActionResult> Get()
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.NavigationItems();

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			return new ObjectResult(result);
		}

		[HttpGet("{id:length(24)}")]
		//[ValidateAntiForgeryToken]
		//[Authorize]
		public async Task<IActionResult> Get(string id)
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.GetById(new ObjectId(id));

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			return new ObjectResult(result);
		}

		// GET api/navigation/sitemap
		[HttpGet("Sitemap")]
		//[ValidateAntiForgeryToken]
		//[OutputCache(Duration = 3600, VaryByParam = "none")]
		//[CacheWebApi(Duration = 3600)]
		public async Task<IActionResult> Sitemap()
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.NavigationSitemap();

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			return new ObjectResult(result);
		}

		[HttpGet("List")]
		public async Task<IActionResult> List()
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.NavigationList();

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			return new ObjectResult(result);
		}
	}
}