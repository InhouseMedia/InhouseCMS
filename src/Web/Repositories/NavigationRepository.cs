namespace Web.Repositories
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;

	using Library.Models;
    using Web.Connections;
    using System.Globalization;
    using System.Threading.Tasks;

    public interface INavigationRepository
	{
		NavigationSitemap GetNavigationItem(string path);
		Task<IEnumerable<NavigationSitemap>> GetNavigation();
	}

	public class NavigationRepository : ConnectionRepository, INavigationRepository
	{
		private readonly IEnumerable<NavigationSitemap> _navigation;

	    public NavigationRepository(ApiConnection api) : base(api)
	    {
			var locale = CultureInfo.DefaultThreadCurrentUICulture.Name;
			var conn = _api.Connect(locale + "/navigation/list");
			var result = conn.Content.ReadAsAsync<IEnumerable<NavigationSitemap>>();
		    _navigation = result.Result;
	    }

		public NavigationSitemap GetNavigationItem(string path)
		{
			return _navigation.FirstOrDefault(n => n.Url.ToLower().Equals(path.ToLower()));
		}

		public async Task<IEnumerable<NavigationSitemap>> GetNavigation()
		{
			var locale = CultureInfo.DefaultThreadCurrentUICulture.Name;
			var conn = await _api.ConnectAsync(locale + "/navigation/sitemap");
			var result = conn.Content.ReadAsAsync<IEnumerable<NavigationSitemap>>();
			return await result;
		}
	}
}