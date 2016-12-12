namespace Web.ViewComponents
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;
	using Library.Models;

	public class TextViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(ArticleContent item)
		{
			return View(item);
		}
	}
}
