namespace Cms.ViewComponents.Article
{
	using Library.Models;

	using Microsoft.AspNetCore.Mvc;

	using System.Threading.Tasks;

	public class CarouselViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(ArticleContent model)
		{
			return View("~/Views/Article/Components/Carousel/Default.cshtml", model);
		}
	}
}