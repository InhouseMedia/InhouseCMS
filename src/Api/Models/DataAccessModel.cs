namespace Api.Models
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public class DataAccess
    {
		private readonly Settings _settings;
        private MongoClient _client;
        private IMongoDatabase _db;

        public DataAccess(IOptions<Settings> settings)
        {
            _settings = settings.Value;

			_client = new MongoClient(_settings.MongoConnection);
            _db = _client.GetDatabase(_settings.Database);
        }

        public IMongoDatabase Connect(){
            return _db;
        }
	}
}