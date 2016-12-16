namespace Web.ViewComponents
{
	using Microsoft.AspNetCore.Mvc;

	public class BackToTopViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}