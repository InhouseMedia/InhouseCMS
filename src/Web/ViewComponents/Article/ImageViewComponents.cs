namespace Web.ViewComponents.Article
{
	using Newtonsoft.Json;

	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	using Library.Models;

	public class ImageViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(ArticleContent model)
		{
			var imageSetting = JsonConvert.DeserializeObject<ImageModel>(model.Code);

			ViewBag.ArticleId = model.ArticleId;
			ViewBag.ContentId = model.Id;
			ViewBag.Action = model.Action;

			return View(imageSetting);
		}
	}
}
