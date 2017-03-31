namespace Api.Controllers
{
	using Api.Repositories;

	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;

	using MongoDB.Bson;

	[Route("[controller]")]
	public class ArticleController : Controller
	{
		private readonly IArticleRepository _repository;

		public ArticleController(IArticleRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		//[ValidateAntiForgeryToken]
		//[Authorize]
		public async Task<IActionResult> Get()
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.Articles();

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

		[HttpGet("content/{id:length(24)}")]
		//[ValidateAntiForgeryToken]
		//[Authorize]
		public async Task<IActionResult> GetContent(string id)
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.GetContent(new ObjectId(id));

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			return new ObjectResult(result);
		}

		[HttpGet("{id:length(24)}/content")]
		//[ValidateAntiForgeryToken]
		//[Authorize]
		public async Task<IActionResult> Page(string id)
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.GetPage(new ObjectId(id));

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			return new ObjectResult(result);
		}

		[HttpGet("{aid:length(24)}/content/{id:length(24)}")]
		//[ValidateAntiForgeryToken]
		//[Authorize]
		public async Task<IActionResult> GetPageContent(string id, string aid)
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.GetPageContent(new ObjectId(id), new ObjectId(aid));

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			return new ObjectResult(result);
		}
	}
}