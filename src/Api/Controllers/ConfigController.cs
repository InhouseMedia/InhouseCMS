namespace Api.Controllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;

	using Api.Config;
	using Api.Repositories;

    [Route("[controller]")]
    public class ConfigController : Controller
    {
        readonly IConfigRepository _repository;

        public ConfigController(IConfigRepository settings)
        {   
            _repository = settings;
        }
     
        [HttpGet]
		//[ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> Get()
        {
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var results = await _repository.Config();

			if (results == null)
				return new StatusCodeResult(204); // 204 No Content
			
			return new ObjectResult(results);
        }
	}
}