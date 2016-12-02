using System;
namespace Web.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;

	public class BoxController : Controller
    {
		public BoxController() { }

		/*private readonly ClientApiClass _clientApi;

		public BoxController()
		{
			var domainName = System.Web.HttpContext.Current.Request.Url.Host;

			var webApiUrl = ConfigurationManager.AppSettings["WebApiUrl"];

			_clientApi = new ClientApiClass(webApiUrl, domainName);
		}

		public IEnumerable<BoxModel> Selection(int placement, int articleId, string template)
		{
			var response = _clientApi.GetResultsSync("api/box");

			// Error
			if (!response.IsSuccessStatusCode) return null;

			var boxes = response.Content.ReadAsAsync<IEnumerable<BoxModel>>().Result;

			// Error
			if (boxes == null) return null;

			return boxes.Where(b => b.Placement.Equals(placement) && (b.ArticleId.Equals(articleId) || b.ArticleId == null) && (b.Template == template.ToLower() || b.Template == null)).ToList();
		}
		
		public ActionResult Homebutton()
		{
			var isHomepage = (System.Web.HttpContext.Current.Request.Url.AbsolutePath == "/");

			return View(isHomepage);
		}

		public ActionResult Contact()
		{
			// Build-in a check to see if you are able to make a phonecall (Jabber, mobile phone, skype etc)
			// otherwise add email address as url instead of the phonenumber.
			var isMobile = (Request.Browser.IsMobileDevice);

			return View(isMobile);
		}*/

		public ActionResult BackToTop()
		{
			return View();
		}

		public ActionResult Footer()
		{
			return View();
		}
		/*
		public ActionResult Maps()
		{
			var company = MvcApplication.Config.Company;

			return View(company);
		}*/
	}
}
