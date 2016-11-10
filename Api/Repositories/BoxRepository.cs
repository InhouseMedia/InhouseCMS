namespace api.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using Microsoft.Extensions.Options;

    using MongoDB.Driver;
    using MongoDB.Bson;
    
    using api.Models;
    
    public interface IBoxRepository
    {
        Task<IEnumerable<Box>> Boxes();
        Task<Box> GetById(ObjectId id);
        Task<IEnumerable<Box>> BoxList();
    }

    public class BoxRepository : IBoxRepository
    {
        private readonly Settings _settings;
        private readonly IMongoDatabase _database;
		readonly DateTime? _today;

        public BoxRepository(IOptions<Settings> settings)
        {
            _settings = settings.Value;
            _database = Connect();
			_today = DateTime.UtcNow;
        }
		
        public async Task<IEnumerable<Box>> Boxes()
        {
            var conn = _database.GetCollection<Box>("Box");
            var temp = await conn.Find(_=>true).ToListAsync();
            return temp.ToArray();
        }

        public async Task<Box> GetById(ObjectId id)
        {
			var builder = Builders<Box>.Filter; 
            var filter = builder.Eq("Id", id);
			var conn = _database.GetCollection<Box>("Box");
			var temp = await conn.Find(filter).FirstOrDefaultAsync();
			return temp;	
        }

        public async Task<IEnumerable<Box>> BoxList()
        {
            var builderSort = Builders<Box>.Sort;
			var sort = builderSort.Ascending("Placement").Ascending("Level");

			var builderFilter = Builders<Box>.Filter;
            var filter = builderFilter.Eq("Active", true) &
						(builderFilter.Lte("PublishDate", _today) |
                        builderFilter.Eq(e => e.ExpireDate, null) ) &
						(builderFilter.Gte("ExpireDate", _today) | 
						builderFilter.Eq(e => e.ExpireDate, null) );
            var conn = _database.GetCollection<Box>("Box");
            var temp = await conn.Find(filter).Sort(sort).ToListAsync();
            return temp.ToArray();
        }

        private IMongoDatabase Connect()
        {
            var client = new MongoClient(_settings.MongoConnection);
            var database = client.GetDatabase(_settings.Database);
            return database;
        }
    }
}