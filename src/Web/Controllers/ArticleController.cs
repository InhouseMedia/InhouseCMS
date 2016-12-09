namespace Web.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
    
	using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;

    using Web.Repositories;

    public class ArticleController : Controller
    {
        private readonly IStringLocalizer<ArticleController> _localizer;
        private readonly IArticleRepository _repository;

        public ArticleController(IArticleRepository repository, IStringLocalizer<ArticleController> localizer)
        {
            _localizer = localizer;
            _repository = repository;
        }

        public async Task<IActionResult> Index(string id)
        {
            if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

            var results = await _repository.GetPage(id);
            
            if (results == null)
				return new StatusCodeResult(204); // 204 No Content

            return View(results);
        }

      
    }
}
