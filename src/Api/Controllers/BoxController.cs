namespace Api.Controllers
{
	using Api.Repositories;

	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;

	using MongoDB.Bson;

	[Route("[controller]")]
	public class BoxController : Controller
	{
		private readonly IBoxRepository _repository;

		public BoxController(IBoxRepository repository)
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

			var result = await _repository.Boxes();

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

		[HttpGet("List")]
		//[CacheWebApi(Duration = 3600)]
		public async Task<IActionResult> List()
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.BoxList();

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			return new ObjectResult(result);
		}
	}
}