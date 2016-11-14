namespace Api.Repositories
{
    using System;
    using System.Reflection;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using Microsoft.Extensions.Options;

    using MongoDB.Driver;
    using MongoDB.Bson;
    
	using Api.Config;
    using Api.Models;
    
    public interface IConfigRepository
    {
        Task<Config> Config();
    }

    public class ConfigRepository : IConfigRepository
    {
		private readonly Config _config;
        private readonly Settings _settings;
        private readonly IMongoDatabase _database;

        public ConfigRepository(IOptions<Settings> settings, IOptions<Config> config)
        {
			_config = config.Value;
            _settings = settings.Value;
            _database = Connect();
        }
		
        public async Task<Config> Config()
        {
            var conn = _database.GetCollection<Config>("Config");
            var temp = await conn.Find(_=>true).FirstOrDefaultAsync();

            //return the modified copy of Target			
            Merge(_config, temp);

            return _config;
        }

        public static Config Merge<Config>(Config target, Config source)
        {
            typeof(Config)
                .GetProperties()
                .Select((PropertyInfo x) => new KeyValuePair<PropertyInfo, object>(x, x.GetValue(source, null)))
                .Where((KeyValuePair<PropertyInfo, object> x) => x.Value != null).ToList()
                .ForEach((KeyValuePair<PropertyInfo, object> x) => x.Key.SetValue(target, x.Value, null));

            return target;  
        }

        private IMongoDatabase Connect()
        {
            var client = new MongoClient(_settings.MongoConnection);
            var database = client.GetDatabase(_settings.Database);
            return database;
        }
    }
}