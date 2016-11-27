namespace Api.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using MongoDB.Driver;
    using MongoDB.Bson;

    using Api.Models;

    public interface INavigationRepository
	{
		Task<IEnumerable<NavigationItem>> NavigationItems();
        Task<NavigationItem> GetById(ObjectId id);
		Task<IEnumerable<NavigationSitemap>> NavigationSitemap();		
        Task<IEnumerable<NavigationSitemap>> NavigationList();
	}

    public class NavigationRepository : INavigationRepository
    {
        private readonly IMongoDatabase _database;
        public static IEnumerable<NavigationItem> ActiveNavigationItems;
		public static List<NavigationSitemap> ActiveNavigationItemsFlat;
		
        public NavigationRepository(DataAccess access)
        {
            _database = access.Connect();
		}

        public async Task<IEnumerable<NavigationItem>> NavigationItems()
        {
            var conn = _database.GetCollection<NavigationItem>("Navigation");
            var temp = await conn.Find(_=>true).ToListAsync();
            return temp.ToArray();
        }
		
        public async Task<NavigationItem> GetById(ObjectId id)
        {
			var builder = Builders<NavigationItem>.Filter; 
            var filter = builder.Eq("Id", id) &
						builder.Eq("Locale", CultureInfo.CurrentUICulture.Name);
			var conn = _database.GetCollection<NavigationItem>("Navigation");
			var temp = await conn.Find(filter).FirstOrDefaultAsync();
			return temp;
        }

        public async Task<IEnumerable<NavigationSitemap>> NavigationSitemap()
		{

			ActiveNavigationItemsFlat = new List<NavigationSitemap>();
			
			var builderSort = Builders<NavigationItem>.Sort;
			var sort = builderSort.Ascending("ParentId").Ascending("Level");


			var builderFilter = Builders<NavigationItem>.Filter;
			var filter = builderFilter.Eq("Active", true) & 
						builderFilter.Lte("CreatedDate", DateTime.UtcNow) &
						builderFilter.Eq("Locale", CultureInfo.CurrentUICulture.Name);
			var conn = _database.GetCollection<NavigationItem>("Navigation");
			ActiveNavigationItems = await conn.Find(filter).Sort(sort).ToListAsync();
			
			var baseUrls = ActiveNavigationItems.Where(b => b.ParentId == null).OrderBy(x => x.Level).ToList();
			
			return SitemapItems(baseUrls).ToArray();	
		}
		
		public async Task<IEnumerable<NavigationSitemap>> NavigationList()
		{
			if (ActiveNavigationItemsFlat != null && ActiveNavigationItems.Any()) return ActiveNavigationItemsFlat.ToArray();

			ActiveNavigationItemsFlat = new List<NavigationSitemap>();
			await NavigationSitemap();

			return ActiveNavigationItemsFlat.ToArray();
		}
			
		private static IEnumerable<NavigationSitemap> SitemapItems(IEnumerable<NavigationItem> navigation, string parentUrl = "/")
		{
			var result = new List<NavigationSitemap>();

			foreach (var item in navigation)
			{
				var tempExtra = (parentUrl == "/") ? "" : "/";
				var tempSub = ActiveNavigationItems.Where(n => n.ParentId == item.Id).OrderBy(x => x.Level).ToList();

				var siteMapItem = ToSiteMapItem(item);
					siteMapItem.Url = parentUrl + tempExtra + siteMapItem.Url;
					
				// Flat List of all active menuitems that we can use to search the correct ArticleId to a specific Url
				var tempItem = siteMapItem.Clone();
					tempItem.ChildLocations = null;	
					
					ActiveNavigationItemsFlat.Add(tempItem);
					
					siteMapItem.ChildLocations = SitemapItems(tempSub, siteMapItem.Url);
					result.Add(siteMapItem);
			}
			return result;
		}
		
		private static NavigationSitemap ToSiteMapItem(NavigationItem item)
		{
			return new NavigationSitemap()
			{
				Title = item.Title,
				Url = item.Url,
				OnClick = item.OnClick,
				Priority = item.Priority,
				//PublishDate = item.PublishDate,
				ChildLocations = { }
			};	
		}
    }
}