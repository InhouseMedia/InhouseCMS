using Web.Repositories;

namespace Web.Connections
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Routing;
	using Microsoft.Extensions.Caching.Memory;

	public interface IRouteConnection : IRouter
	{
	}

	public class RouteConnection : IRouteConnection
	{
		private readonly IMemoryCache _cache;
		private object _synclock = new object();
		private readonly IArticleRepository _article;
		private readonly INavigationRepository _navigation;

		public RouteConnection(IMemoryCache cache, IArticleRepository article, INavigationRepository navigation)
		{
			_article = article;
			_cache = cache;
			_navigation = navigation;
		}

		public Task RouteAsync(RouteContext context)
		{
			var path = context.HttpContext.Request.Path.Value ?? "/";
			var navItem = _navigation.GetNavigationItem(path);
			var articleId = navItem?.ArticleId ?? "012345678901234567890123"; // fake articleId needs to be 24 chars long

			var articleItem = _article.GetPage(articleId);

			var task = new Task(() =>
			{
				var routeData = new RouteData();
				routeData.Values["controller"] = articleItem.Result.Controller;
				routeData.Values["action"] = articleItem.Result.Action;

				context.RouteData = routeData;
			});

			task.Start();

			return task;
		}

		public VirtualPathData GetVirtualPath(VirtualPathContext context)
		{
			throw new NotImplementedException();
		}
	}
}
