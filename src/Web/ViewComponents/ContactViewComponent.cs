namespace Web.ViewComponents
{
	using Microsoft.AspNetCore.Mvc;

	public class ContactViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}