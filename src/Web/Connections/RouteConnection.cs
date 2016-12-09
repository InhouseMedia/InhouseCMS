using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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

		public RouteConnection(IMemoryCache cache)
		{
			_cache = cache;
		}

		public Task RouteAsync(RouteContext context)
		{
			var navigation = context.HttpContext.RequestServices.GetRequiredService<INavigationRepository>();
			var article = context.HttpContext.RequestServices.GetRequiredService<IArticleRepository>();
			
			var path = context.HttpContext.Request.Path.Value ?? "/";
			var navItem = navigation.GetNavigationItem(path);
			var articleId = navItem?.ArticleId ?? "012345678901234567890123"; // fake articleId needs to be 24 chars long

			var articleItem = article.GetPage(articleId);
			
			var task = new Task(() =>
			{

				var routeData = new RouteData(context.RouteData);
				routeData.Values["controller"] = articleItem.Controller;
				routeData.Values["action"] = articleItem.Action;

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
