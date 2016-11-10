namespace api.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using Microsoft.Extensions.Options;

    using MongoDB.Driver;
    using MongoDB.Bson;
    
    using api.Models;
    
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> Articles();
        Task<Article> GetById(ObjectId id);
        Task<ArticlePage> GetPage(ObjectId id);
    }

    public class ArticleRepository : IArticleRepository
    {
        private readonly Settings _settings;
        private readonly IMongoDatabase _database;
		private readonly string _locale;
		readonly DateTime? _today;

        public ArticleRepository(IOptions<Settings> settings)
        {
            _settings = settings.Value;
            _database = Connect();
			_locale = "nl-NL";
			_today = DateTime.UtcNow;
        }
		
        public async Task<IEnumerable<Article>> Articles()
        {
			//var filter = Builders<Article>.Filter.Eq("Locale", _locale);
            var conn = _database.GetCollection<Article>("Article");
            var temp = await conn.Find(_=>true).ToListAsync();
            return temp.ToArray();
        }

        public async Task<Article> GetById(ObjectId id)
        {
			var builder = Builders<Article>.Filter; 
            var filter = builder.Eq("Id", id) &
						builder.Eq("Locale", _locale);
			var conn = _database.GetCollection<Article>("Article");
			var temp = await conn.Find(filter).FirstOrDefaultAsync();
			return temp;	
        }
		
		public async Task<ArticlePage> GetPage(ObjectId id)
        {
			var builderSort = Builders<Article>.Sort;
			var sort = builderSort.Ascending("CreatedDate").Ascending("PublishDate");
			
			var builderFilter = Builders<Article>.Filter; 
            var filter = (builderFilter.Eq("Id", id) | builderFilter.Eq("Controller", "Maintenance")) &
						builderFilter.Eq("Active", true) &
						builderFilter.Lte("PublishDate", _today) &
						(builderFilter.Gt("ExpireDate", _today) | 
						builderFilter.Eq(e => e.ExpireDate, null) ) &
						builderFilter.Eq("Locale", _locale);
			var conn = _database.GetCollection<Article>("Article");
			var temp = await conn.Find(filter).Sort(sort).FirstOrDefaultAsync();

            var content = ToArticlePage(temp);
            
            var contentSort = Builders<ArticleContent>.Sort.Ascending("Level");
            var contentFilter = Builders<ArticleContent>.Filter.Eq("ArticleId", content.Id);
            var connect = _database.GetCollection<ArticleContent>("Article_Content");
			
            var tempContent = await connect.Find(contentFilter).Sort(contentSort).ToListAsync();

            content.ArticleContent = tempContent.ToArray();
			return content;	
        }

        private static ArticlePage ToArticlePage(Article item)
		{
			return new ArticlePage()
			{
				Id = item.Id,
				UserId = item.UserId,
				MetaTitle = item.MetaTitle,
				MetaDescription = item.MetaDescription,
				MetaKeywords = item.MetaKeywords,
				Controller = item.Controller,
				Action = item.Action,
				Locale = item.Locale,
				Active = item.Active,
				PublishDate = item.PublishDate,
				ExpireDate = item.ExpireDate,
				CreatedDate = item.CreatedDate,
				ChangedDate = item.ChangedDate,
				ArticleContent = { }
			};
		}

        private IMongoDatabase Connect()
        {
            var client = new MongoClient(_settings.MongoConnection);
            var database = client.GetDatabase(_settings.Database);
            return database;
        }
    }
}