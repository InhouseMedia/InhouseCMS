namespace Cms.ViewComponents.Article
{
	using Library.Models;

	using Microsoft.AspNetCore.Mvc;

	using System.Threading.Tasks;

	public class PanelHeaderViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(ArticleContent model)
		{
			return View(model);
		}
	}
}