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
   public class ArticleController : Controller
    {
        readonly IArticleRepository _articleRepository;

        public ArticleController(IArticleRepository settings)
        {   
            _articleRepository = settings;
        }
     
        [HttpGet]
		//[ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> Get()
        {
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var results = await _articleRepository.Articles() as IEnumerable<Article>;

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
				
            var results = await _articleRepository.GetById(new ObjectId(id)) as Article;
			
            if (results == null)
				return new StatusCodeResult(204); // 204 No Content

            return new ObjectResult(results);
        }
		
		[HttpGet("Page/{id:length(24)}")]
		//[ValidateAntiForgeryToken]
		//[Authorize]
        public async Task<IActionResult> Page(string id)
        {
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error
				
            var results = await _articleRepository.GetPage(new ObjectId(id)) as Article;
			
            if (results == null)
				return new StatusCodeResult(204); // 204 No Content

            return new ObjectResult(results);
		}
    }
}