namespace Web.ViewComponents.Article
{
	using Newtonsoft.Json;

	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Library.Models;

	public class SectionViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(ArticleContent model)
		{
			var sectionModel = JsonConvert.DeserializeObject<List<SectionModel>>(model.Code);

			ViewBag.ArticleId = model.ArticleId;
			ViewBag.ContentId = model.Id;
			ViewBag.Action = model.Action;

			return View(sectionModel);
		}
	}
}
