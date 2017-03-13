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
			var conn = _database.GetCollection<Product>("Product");
			var result = await conn.Find(_ => true).ToListAsync();
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