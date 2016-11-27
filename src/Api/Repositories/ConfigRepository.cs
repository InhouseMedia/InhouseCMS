namespace Api.Repositories
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Threading.Tasks;
	
	using Microsoft.Extensions.Options;
	using MongoDB.Driver;

	using Api.Models;
	using Api.Config;

	public interface IConfigRepository
    {
        Task<Config> Config();
    }

    public class ConfigRepository : IConfigRepository
    {
		private readonly Config _config;
        private readonly IMongoDatabase _database;

        public ConfigRepository(DataAccess access, IOptions<Config> config)
        {
			_config = config.Value;
            _database = access.Connect();
        }
		
        public async Task<Config> Config()
        {
            var conn = _database.GetCollection<Config>("Config");
            var temp = await conn.Find(_=>true).FirstOrDefaultAsync();

            //return the modified copy of Target			
            Merge(_config, temp);

            return _config;
        }

        public static Config Merge(Config target, Config source)
        {
            typeof(Config)
                .GetProperties()
                .Select(x => new KeyValuePair<PropertyInfo, object>(x, x.GetValue(source, null)))
                .Where(x => x.Value != null).ToList()
                .ForEach(x => x.Key.SetValue(target, x.Value, null));

            return target;  
        }
    }
}