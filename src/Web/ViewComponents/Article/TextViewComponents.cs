namespace Web.ViewComponents.Article
{
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	using Library.Models;

	public class TextViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(ArticleContent model)
		{
			return View("/Views/Article/Components/Text/Default.cshtml",model);
		}
	}
}
