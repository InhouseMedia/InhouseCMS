namespace Cms.Controllers
{
	using Library.Config;
	using Library.Models;

	using Microsoft.AspNetCore.Mvc;

	using System.Threading;
	using System.Threading.Tasks;
	public class ArticleController : Controller
	{


		public ArticleController(){}

		public async Task<IActionResult> Index(string id)
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			//var result = await _repository.GetPage(id);


			return View();
		}
	}
}