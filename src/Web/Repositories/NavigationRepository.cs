namespace Web.Repositories
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;

	using Library.Models;
    using Web.Connections;
    using System.Globalization;

    public interface INavigationRepository
	{
		NavigationSitemap GetNavigationItem(string path);
	}

	public class NavigationRepository : ConnectionRepository, INavigationRepository
	{
		private readonly IEnumerable<NavigationSitemap> _navigation;

	    public NavigationRepository(ApiConnection api) : base(api)
	    {
			var locale = CultureInfo.DefaultThreadCurrentUICulture.Name;
			var conn = _api.Connect(locale + "/navigation/list");
			var test = conn.Content.ReadAsAsync<IEnumerable<NavigationSitemap>>();
		    _navigation = test.Result;
	    }

		public NavigationSitemap GetNavigationItem(string path)
		{
			return _navigation.FirstOrDefault(n => n.Url.ToLower().Equals(path.ToLower()));
		}
	}
}
