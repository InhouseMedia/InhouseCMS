namespace Api.Repositories
{
	using MongoDB.Driver;

	using Api.Connections;

	public class ConnectionRepository
	{
		protected readonly IMongoDatabase _database;

		public ConnectionRepository(IDatabaseConnection database)
		{
			_database = database.Connect();
		}
	}
}