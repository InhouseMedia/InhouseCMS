namespace api.Repositories
{
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using MongoDB.Driver;
    using MongoDB.Bson;

    using api.Models;
    using api;
    
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> Articles();
        Task<Article> GetById(ObjectId id);
        Task<Article> GetPage(ObjectId id);
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
			var filter = Builders<Article>.Filter.Eq("Locale", _locale);
            var test = _database.GetCollection<Article>("Article");
            var temp = await test.Find(_=>true).ToListAsync();
            return temp.ToArray();
        }

        public async Task<Article> GetById(ObjectId id)
        {
			var builder = Builders<Article>.Filter; 
            var filter = builder.Eq("Id", id) &
						builder.Eq("Locale", _locale);
			var test = _database.GetCollection<Article>("Article");
			var temp = await test.Find(filter).FirstOrDefaultAsync();
			return temp;	
        }
		
		public async Task<Article> GetPage(ObjectId id)
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
			var test = _database.GetCollection<Article>("Article");
			var temp = await test.Find(filter).Sort(sort).FirstOrDefaultAsync();
			return temp;	
        }

        private IMongoDatabase Connect()
        {
            var client = new MongoClient(_settings.MongoConnection);
            var database = client.GetDatabase(_settings.Database);
            return database;
        }
    }
}