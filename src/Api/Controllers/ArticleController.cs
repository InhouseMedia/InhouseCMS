namespace Api.Controllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;

 	using MongoDB.Bson;

	using Api.Repositories;

    [Route("[controller]")]
    public class ArticleController : Controller
    {
	    private readonly IArticleRepository _repository;

        public ArticleController(IArticleRepository settings)
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

			var results = await _repository.Articles();

			if (results == null)
				return new StatusCodeResult(204); // 204 No Content
			
			return new ObjectResult(results);
        }

        [HttpGet("{id:length(24)}")]
		//[ValidateAntiForgeryToken]
		//[Authorize]
        public async Task<IActionResult> Get(string id)
        {
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error
				
            var results = await _repository.GetById(new ObjectId(id));
			
            if (results == null)
				return new StatusCodeResult(204); // 204 No Content

            return new ObjectResult(results);
        }
		
		[HttpGet("{id:length(24)}/content")]
		//[ValidateAntiForgeryToken]
		//[Authorize]
        public async Task<IActionResult> Page(string id)
        {
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error
				
            var results = await _repository.GetPage(new ObjectId(id));
			
            if (results == null)
				return new StatusCodeResult(204); // 204 No Content

            return new ObjectResult(results);
		}
    }
}