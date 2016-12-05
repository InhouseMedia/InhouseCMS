namespace Api.Connections
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

    using MongoDB.Driver;

    using Library.Models;

    public class DatabaseConnection
    {
        private readonly IMongoDatabase _db;

        public DatabaseConnection(IOptions<Database> database, IHttpContextAccessor httpContextAccessor)
        {
            var connection = database.Value;
            var databaseName = (string)httpContextAccessor.HttpContext.Request.Headers["ConnectionKey"] ?? connection.DatabaseName;

            var client = new MongoClient(connection.MongoConnection);
            _db = client.GetDatabase(databaseName);
        }

        public IMongoDatabase Connect()
        {
            return _db;
        }
    }
}