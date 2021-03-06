namespace Cms.ViewComponents
{
	using Library.Models;
	
	using Microsoft.AspNetCore.Mvc;

	using System.Threading.Tasks;

	public class PublishLogsViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(ArticlePage model)
		{
			return View(model);
		}
	}
}