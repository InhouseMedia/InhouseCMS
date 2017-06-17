namespace Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Localization;

	using MailKit;
	using MailKit.Net.Smtp;
	using MimeKit;

	using Newtonsoft.Json;

	using System;
	using System.Net;
	using System.Text.RegularExpressions;
	using System.Threading;
	using System.Threading.Tasks;

	using Library.Config;
	using Library.Models;
	using Library.Repositories;

    //Done in startup
    //[ServiceFilter(typeof(LocalizationActionFilter))]
    public class MaintenanceController : WebController
    {

		public MaintenanceController(IArticleRepository repository, IStringLocalizer<ArticleController> localizer, ConfigRepository config)
		{
			_localizer = localizer;
			_repository = repository;
			_config = config.GetConfig();
		}
		
    }
}