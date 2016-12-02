namespace Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    
    using Web.Filters;

    [ServiceFilter(typeof(LocalizationActionFilter))]
    public class HomeController : Controller
    {
		private readonly IStringLocalizer<HomeController> _localizer;
	
        public HomeController(IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = _localizer["AboutTitle"];

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
