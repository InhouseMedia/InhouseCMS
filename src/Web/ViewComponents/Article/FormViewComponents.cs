namespace Web.ViewComponents
{
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	using Library.Models;

	public class FormViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(ArticleContent item)
		{
			return View(item);
		}
	}
}
