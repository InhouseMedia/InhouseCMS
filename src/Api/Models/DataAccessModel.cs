namespace Api.Models
{
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.Options;

	using MongoDB.Driver;

    public class DataAccess
    {
	    private readonly IMongoDatabase _db;

        public DataAccess(IOptions<Settings> settings, IHttpContextAccessor httpContextAccessor)
        {

			var connection = settings.Value;
			var database = (string)httpContextAccessor.HttpContext.Request.Headers["ConnectionKey"] ?? connection.Database;

			var client = new MongoClient(connection.MongoConnection);
            _db = client.GetDatabase(database);
        }

        public IMongoDatabase Connect(){
            return _db;
        }
	}
}