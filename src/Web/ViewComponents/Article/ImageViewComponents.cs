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
			//TODO: Maybe create a ArticleContentImage Model and use that in the view instead of a bunch of ViewBags
			var imageSetting = JsonConvert.DeserializeObject<ImageModel>(model.Code);

			ViewBag.ArticleId = model.ArticleId;
			ViewBag.ContentId = model.Id;
			ViewBag.Action = model.Action;
			ViewBag.Title = model.Title;
			ViewBag.Text = model.Text;

			return View(imageSetting);
		}
	}
}
