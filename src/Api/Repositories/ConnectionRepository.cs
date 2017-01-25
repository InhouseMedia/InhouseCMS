namespace Api.Repositories
{
	using MongoDB.Driver;

	using Api.Connections;

	public class ConnectionRepository
	{
		protected readonly IMongoDatabase _database;

		public ConnectionRepository(DatabaseConnection database)
		{
			_database = database.Connect();
		}
	}
}