namespace Web.ViewComponents
{
	using Microsoft.AspNetCore.Mvc;

	public class CarouselViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
