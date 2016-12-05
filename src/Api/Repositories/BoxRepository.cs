namespace Api.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MongoDB.Driver;
    using MongoDB.Bson;

    using Api.Models;
    using Api.Connections;

    public interface IBoxRepository
    {
        Task<IEnumerable<Box>> Boxes();
        Task<Box> GetById(ObjectId id);
        Task<IEnumerable<Box>> BoxList();
    }

    public class BoxRepository : ConnectionRepository, IBoxRepository
    {
        public BoxRepository(DatabaseConnection database) : base(database)
        {
        }
        
        public async Task<IEnumerable<Box>> Boxes()
        {
            var conn = _database.GetCollection<Box>("Box");
            var temp = await conn.Find(_ => true).ToListAsync();
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
                        (builderFilter.Lte("PublishDate", DateTime.UtcNow) |
                        builderFilter.Eq(e => e.ExpireDate, null)) &
                        (builderFilter.Gte("ExpireDate", DateTime.UtcNow) |
                        builderFilter.Eq(e => e.ExpireDate, null));
            var conn = _database.GetCollection<Box>("Box");
            var temp = await conn.Find(filter).Sort(sort).ToListAsync();
            return temp.ToArray();
        }
    }
}