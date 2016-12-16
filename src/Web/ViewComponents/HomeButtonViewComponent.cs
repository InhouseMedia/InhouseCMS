namespace Web.ViewComponents
{
	using Microsoft.AspNetCore.Mvc;

	public class HomeButtonViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			var isHomepage = HttpContext.Request.Path.ToString() == "/";
			return View(isHomepage);
		}
	}
}