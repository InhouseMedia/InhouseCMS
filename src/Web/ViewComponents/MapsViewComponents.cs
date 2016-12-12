namespace Web.ViewComponents
{
	using Microsoft.AspNetCore.Mvc;

	public class MapsViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
