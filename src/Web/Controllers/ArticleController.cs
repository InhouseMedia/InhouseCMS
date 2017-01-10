namespace Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;

	using Newtonsoft.Json;

	using System.Text.RegularExpressions;
	using System.Threading.Tasks;

	using Library.Models;
	using Web.Repositories;
	
	//Done in startup
	//[ServiceFilter(typeof(LocalizationActionFilter))]
	public class ArticleController : Controller
    {
        private readonly IStringLocalizer<ArticleController> _localizer;
        private readonly IArticleRepository _repository;

        public ArticleController(IArticleRepository repository, IStringLocalizer<ArticleController> localizer)
        {
            _localizer = localizer;
            _repository = repository;
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

			var body = "";
			var articleId = Request.Form["ArticleId"];
			var contentId = Request.Form["ContentId"];

			var response = await _repository.GetContent(articleId, contentId);
			// Error
			if (response == null)
				return new StatusCodeResult(204); // 204 No Content

			var formOptions = JsonConvert.DeserializeObject<FormModel>(response.Code);
			
			foreach (var field in formOptions.Fields)
			{
				var name = new Regex("[\\s_-]").Replace(field.Label, "");
				body += "<p><b>" + field.Label + ":</b> " + Request.Form[name];
			}
			/*
			var message = new MailMessage();
			message.From = new MailAddress(MvcApplication.Config.Company.Contact.Email);
			message.To.Add(new MailAddress(formOptions.Mail.To)); //Should be from code (json object)
			message.Body = body;
			message.Subject = formOptions.Mail.Subject;
			message.IsBodyHtml = true;

			try
			{
				SmtpClient client = new SmtpClient();
				client.Port = 25;
				client.Host = "smtp.gmail.com"; //or SMTP name
				client.Timeout = 10000;
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.UseDefaultCredentials = false;
				client.Credentials = new NetworkCredential("ramonklanke@gmail.com", "Evelienklein@1");
				client.Send(message);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("No SMTP Mail sent", ex);
				//MessageBox.Show(ex.Message);
			}*/

			return View();
		}
	}
}