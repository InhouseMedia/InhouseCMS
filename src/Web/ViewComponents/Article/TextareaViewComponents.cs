namespace Web.ViewComponents
{
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

	using Library.Models;

	public class TextareaViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(FieldsModel model)
		{
			return View(model);
		}
	}
}
