namespace Api.Repositories
{
	using Api.Connections;

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using MongoDB.Bson;
	using MongoDB.Driver;

	using Library.Models;

	public interface IArticleRepository
	{
		Task<IEnumerable<Article>> Articles();
		Task<Article> GetById(ObjectId id);
		Task<ArticlePage> GetPage(ObjectId id);
		Task<ArticleContent> GetContent(ObjectId id);
		Task<ArticleContent> GetPageContent(ObjectId id, ObjectId articleId);
	}

	public class ArticleRepository : ConnectionRepository, IArticleRepository
	{
		public ArticleRepository(IDatabaseConnection database) : base(database)
		{
		}

		public async Task<IEnumerable<Article>> Articles()
		{
			var conn = _database.GetCollection<Article>("Article");
			var result = await conn.Find(_ => true).ToListAsync();
			return result.ToArray();
		}

		public async Task<Article> GetById(ObjectId id)
		{
			var builder = Builders<Article>.Filter;
			var filter = builder.Eq("Id", id);
			var conn = _database.GetCollection<Article>("Article");
			var result = await conn.Find(filter).FirstOrDefaultAsync();
			return result;
		}

		public async Task<ArticleContent> GetContent(ObjectId id)
		{
			var builder = Builders<ArticleContent>.Filter;
			var filter = builder.Eq("Id", id);
			var conn = _database.GetCollection<ArticleContent>("Article_Content");
			var result = await conn.Find(filter).FirstOrDefaultAsync();
			return result;
		}

		public async Task<ArticlePage> GetPage(ObjectId id)
		{
			var builderSort = Builders<Article>.Sort;
			var sort = builderSort.Ascending("CreatedDate").Ascending("PublishDate");

			var builderFilter = Builders<Article>.Filter;
			var filter = (builderFilter.Eq("Id", id) | builderFilter.Eq("Controller", "Maintenance")) &
						builderFilter.Eq("Active", true) &
						builderFilter.Lte("PublishDate", DateTime.UtcNow) &
						(builderFilter.Gt("ExpireDate", DateTime.UtcNow) |
						builderFilter.Eq(e => e.ExpireDate, null));
			var conn = _database.GetCollection<Article>("Article");
			var temp = await conn.Find(filter).Sort(sort).FirstOrDefaultAsync();

			var content = ToArticlePage(temp);

			var contentSort = Builders<ArticleContent>.Sort.Ascending("Level");
			var contentFilter = Builders<ArticleContent>.Filter.Eq("ArticleId", content.Id);
			var connect = _database.GetCollection<ArticleContent>("Article_Content");

			var result = await connect.Find(contentFilter).Sort(contentSort).ToListAsync();

			content.ArticleContent = result.ToArray();
			return content;
		}

		public async Task<ArticleContent> GetPageContent(ObjectId id, ObjectId articleId)
		{
			var builder = Builders<ArticleContent>.Filter;
			var filter = builder.Eq("Id", id) & builder.Eq("ArticleId", articleId);
			var conn = _database.GetCollection<ArticleContent>("Article_Content");
			var result = await conn.Find(filter).FirstOrDefaultAsync();
			return result;
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
	}
}