namespace api.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

 	using MongoDB.Bson;

    using api.Models;
	using api.Repositories;

    [Route("[controller]")]
   	public class NavigationController : Controller
    {
        readonly INavigationRepository _repository;

        public NavigationController(INavigationRepository settings)
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

			var results = await _repository.NavigationItems() as IEnumerable<NavigationItem>;

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
				
            var results = await _repository.GetById(new ObjectId(id)) as NavigationItem;
			
            if (results == null)
				return new StatusCodeResult(204); // 204 No Content

            return new ObjectResult(results);
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

			var result = await _repository.NavigationSitemap() as IEnumerable<NavigationSitemap>;

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			return new ObjectResult(result);
		}
		
		[HttpGet("List")]
		public async Task<IActionResult> List()	
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error
				
            var result = await _repository.NavigationList() as IEnumerable<NavigationSitemap>;
			
            if (result == null)
				return new StatusCodeResult(204); // 204 No Content

            return new ObjectResult(result);
		}
    }
}