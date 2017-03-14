namespace Api.Connections
{
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.Options;

	using MongoDB.Driver;

	using Library.Models;

	public interface IDatabaseConnection
	{
		void SetDatabase();
		IMongoDatabase Connect();
	}

	public class DatabaseConnection : IDatabaseConnection
	{
		private readonly IMongoClient _client;
		private readonly IHttpContextAccessor _context;

		private readonly Database _connection;
		private IMongoDatabase _db;
		private string _databaseName;


		public DatabaseConnection(IOptions<Database> database, IHttpContextAccessor httpContextAccessor)
		{
			_context = httpContextAccessor;
			_connection = database.Value;
			_client = new MongoClient(_connection.MongoConnection);
			SetDatabase();
		}

		public void SetDatabase()
		{
			_databaseName = (string)_context.HttpContext.Request.Headers["ConnectionKey"] ?? _connection.DatabaseName;
			_db = _client.GetDatabase(_databaseName);
		}

		public IMongoDatabase Connect()
		{
			return _db;
		}
	}
}