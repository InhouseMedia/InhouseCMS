namespace Cms.Controllers
{
    using Library.Config;
    using Library.Models;

    using Microsoft.AspNetCore.Mvc;

    using System.Threading;
    using System.Threading.Tasks;

    using Web.Repositories;
    using Microsoft.Extensions.Localization;
    using Web.Controllers;

    public class ArticleController : Web.Controllers.ArticleController
	{
        public ArticleController(IArticleRepository repository, IStringLocalizer<Web.Controllers.ArticleController> localizer, ConfigRepository config) : base(repository, localizer, config)
        {

        }
	}
}