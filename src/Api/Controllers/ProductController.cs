namespace Api.Controllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;

	using MongoDB.Bson;

	using Api.Repositories;

	[Route("[controller]")]
	public class ProductController : Controller
	{
		private readonly IProductRepository _repository;

		public ProductController(IProductRepository repository)
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

			var result = await _repository.Products();

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
	}
}