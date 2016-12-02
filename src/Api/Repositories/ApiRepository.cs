namespace Api.Repositories
{
    using MongoDB.Driver;

    using Api.Connections;

    public class ApiRepository
    {
        protected readonly IMongoDatabase _database;

        public ApiRepository(DatabaseConnection database)
        {
            _database = database.Connect();
        }
    }
}