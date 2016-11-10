namespace api.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;

 	using MongoDB.Bson;

    using api.Models;
	using api.Repositories;

    [Route("[controller]")]
    public class BoxController : Controller
    {
        readonly IBoxRepository _repository;

        public BoxController(IBoxRepository settings)
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

			var results = await _repository.Boxes() as IEnumerable<Box>;

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
				
            var results = await _repository.GetById(new ObjectId(id)) as Box;
			
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
				
            var result = await _repository.BoxList() as IEnumerable<Box>;
			
            if (result == null)
				return new StatusCodeResult(204); // 204 No Content

            return new ObjectResult(result);
		}
    }
}