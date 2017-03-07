namespace Api.Controllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;

	using Api.Repositories;

	[Route("[controller]")]
	public class ConfigController : Controller
	{
		private readonly IConfigRepository _repository;

		public ConfigController(IConfigRepository repository)
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

			var result = await _repository.Config();

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			return new ObjectResult(result);
		}
	}
}