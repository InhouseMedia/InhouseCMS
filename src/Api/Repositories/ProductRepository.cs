namespace Api.Repositories
{
	using Api.Connections;

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using MongoDB.Bson;
	using MongoDB.Driver;

	using Library.Models;

	public interface IProductRepository
	{
		Task<IEnumerable<Product>> Products();
		Task<Product> GetById(ObjectId id);
	}

	public class ProductRepository : ConnectionRepository, IProductRepository
	{
		public ProductRepository(IDatabaseConnection database) : base(database)
		{
		}

		public async Task<IEnumerable<Product>> Products()
		{
			var builderSort = Builders<Product>.Sort;
			var sort = builderSort.Descending("PublishDate");

			var builderFilter = Builders<Product>.Filter;
			var filter = builderFilter.Eq("Active", true) &
						builderFilter.Lte("PublishDate", DateTime.UtcNow) &
						(builderFilter.Gt("ExpireDate", DateTime.UtcNow) |
						builderFilter.Eq(e => e.ExpireDate, null));

			var conn = _database.GetCollection<Product>("Product");
			var result = await conn.Find(filter).Sort(sort).ToListAsync();
			return result.ToArray();
		}

		public async Task<Product> GetById(ObjectId id)
		{
			var builder = Builders<Product>.Filter;
			var filter = builder.Eq("Id", id);
			var conn = _database.GetCollection<Product>("Product");
			var result = await conn.Find(filter).FirstOrDefaultAsync();
			return result;
		}
	}
}