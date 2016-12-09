namespace Web.Connections
{
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Caching.Memory;
	using Microsoft.Extensions.DependencyInjection;
	
	using System.Threading.Tasks;

	using Web.Repositories;

    public interface IRouteConnection : IRouter
	{
	}

	public class RouteConnection : IRouteConnection
	{
		private readonly IMemoryCache _cache;
		private readonly IRouter _defaultRouter;
		private object _synclock = new object();

		public RouteConnection(IMemoryCache cache, IRouter defaultRouter) 
		{
			_cache = cache;
			_defaultRouter = defaultRouter;
		}

		public async Task RouteAsync(RouteContext context)
		{
			var navigation = context.HttpContext.RequestServices.GetRequiredService<INavigationRepository>();
			var article = context.HttpContext.RequestServices.GetRequiredService<IArticleRepository>();
			
			var path = context.HttpContext.Request.Path.Value ?? "/";
			var navItem = navigation.GetNavigationItem(path);
			var articleId = navItem?.ArticleId ?? "012345678901234567890123"; // fake articleId needs to be 24 chars long

			var articleItem = article.GetPage(articleId);

			var oldRouteData = context.RouteData;
			var routeData = new RouteData(oldRouteData);
			routeData.Values["controller"] = articleItem.Result.Controller;
			routeData.Values["action"] = articleItem.Result.Action;
	
			context.RouteData = routeData;
			await _defaultRouter.RouteAsync(context);
		}

		public VirtualPathData GetVirtualPath(VirtualPathContext context)
		{
			//TODO: Probably not correct. should be something like: Article/Index/xxx
			var requestPath = context.HttpContext.Request.Path.Value;
			var result = new VirtualPathData(this, requestPath);
           
			return result;
		}
	}
}
