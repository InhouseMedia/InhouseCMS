namespace Api.Controllers
{
	using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Localization;

 	using MongoDB.Bson;

	using Api.Filters;
	using Api.Repositories;

	[ServiceFilter(typeof(LanguageActionFilter))]
    [Route("[controller]")]
   	public class NavigationController : Controller
    {
        private readonly INavigationRepository _repository;
		private readonly IStringLocalizer<NavigationController> _localizer;

        public NavigationController(INavigationRepository settings, IStringLocalizer<NavigationController> localizer)
        {   
            _repository = settings;
			_localizer = localizer;
        }
     
        [HttpGet]
		//[ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> Get()
        {
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var results = await _repository.NavigationItems();

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

        // GET api/navigation/sitemap
		[HttpGet("Sitemap")]
		//[ValidateAntiForgeryToken]
		//[OutputCache(Duration = 3600, VaryByParam = "none")]
		//[CacheWebApi(Duration = 3600)]
		public async Task<IActionResult> Sitemap()
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.NavigationSitemap();

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			return new ObjectResult(result);
		}
		
		[HttpGet("List")]
		public async Task<IActionResult> List()	
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error
				
            var result = await _repository.NavigationList();
			
            if (result == null)
				return new StatusCodeResult(204); // 204 No Content

            return new ObjectResult(result);
		}
    }
}