namespace Cms.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Localization;

	using System;
	using System.Net;
	using System.Text.RegularExpressions;
	using System.Threading;
	using System.Threading.Tasks;

	using Library.Config;
	using Library.Models;

	[Area("Cms")]
	//Done in startup
	//[ServiceFilter(typeof(LocalizationActionFilter))]
	public class LoginController : Controller
	{
		public LoginController()
		{

		}

		public async Task<IActionResult> Index()
		{

			return View("~/Views/Login/Index.cshtml", null);
		}
	}
}