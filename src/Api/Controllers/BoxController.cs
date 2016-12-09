namespace Api.Controllers
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;

 	using MongoDB.Bson;

	using Api.Repositories;

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

			var results = await _repository.Boxes();

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