namespace Web.ViewComponents
{
	using Newtonsoft.Json;
	using Microsoft.AspNetCore.Mvc;

	using Library.Models;

	public class FormViewComponent : ViewComponent
	{
        public IViewComponentResult Invoke(ArticleContent model)
        {
            if (!ModelState.IsValid)
                return View(); //return new StatusCodeResult(500); // 500 Internal Server Error

            var formFields = JsonConvert.DeserializeObject<FormModel>(model.Code);

			ViewBag.ArticleId = model.ArticleId;
           	ViewBag.ContentId = model.Id;
        	ViewBag.Action = model.Action;

            return View(formFields);
        }
    }
}
