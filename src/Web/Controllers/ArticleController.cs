namespace Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Localization;

	using MailKit.Net.Smtp;
	using MimeKit;

	using Newtonsoft.Json;

	using System;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;

	using Library.Config;
	using Library.Models;
	using Web.Repositories;

	//Done in startup
	//[ServiceFilter(typeof(LocalizationActionFilter))]
	public class ArticleController : Controller
	{
		private readonly SiteConfig _config;
		private readonly IStringLocalizer<ArticleController> _localizer;
		private readonly IArticleRepository _repository;

		public ArticleController(IArticleRepository repository, IStringLocalizer<ArticleController> localizer, ConfigRepository config)
		{
			_localizer = localizer;
			_repository = repository;
			_config = config.GetConfig();
		}

		public async Task<IActionResult> Index(string id)
		{
			if (!ModelState.IsValid)
				return new StatusCodeResult(500); // 500 Internal Server Error

			var result = await _repository.GetPage(id);

			if (result == null)
				return new StatusCodeResult(204); // 204 No Content

			return View(result);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Form()
		{
			if (!ModelState.IsValid) return View();

			var body = new BodyBuilder();
			var articleId = Request.Form["ArticleId"];
			var contentId = Request.Form["ContentId"];

			var response = await _repository.GetPageContent(articleId, contentId);
			
			// Error
			if (response == null)
				return new StatusCodeResult(204); // 204 No Content

			var formOptions = JsonConvert.DeserializeObject<FormModel>(response.Code);

			foreach (var field in formOptions.Fields)
			{
				var name = new Regex("[\\s_-]").Replace(field.Label, "");
				body.HtmlBody += "<p><b>" + field.Label + ":</b> " + Request.Form[name] + "</p>";
			}
			
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(_config.Company.Contact.Email));
			message.To.Add(new MailboxAddress(formOptions.Mail.To)); //Should be from code (json object)
			message.Subject = formOptions.Mail.Subject;
			message.Body = body.ToMessageBody();

			using (var client = new SmtpClient())
			{
				try
				{
					if (!_config.Mailserver.Oauth2) client.AuthenticationMechanisms.Remove("XOAUTH2");

					client.Connect(_config.Mailserver.Smtp, _config.Mailserver.Port, _config.Mailserver.UseSsl);
					client.Authenticate(_config.Mailserver.Account, _config.Mailserver.Password);
					client.Send(message);
					client.Disconnect(true);
				}
				catch (Exception ex)
				{
					return new StatusCodeResult(450);
				}
			}

			return View();
		}
	}
}