namespace Api.Repositories
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Threading.Tasks;
	
	using Microsoft.Extensions.Options;
	using MongoDB.Driver;

	using Api.Models;
	using Library.Config;

	public interface IConfigRepository
    {
        Task<SiteConfig> Config();
    }

    public class ConfigRepository : IConfigRepository
    {
		private readonly SiteConfig _config;
        private readonly IMongoDatabase _database;

        public ConfigRepository(DataAccess access, IOptions<SiteConfig> config)
        {
			_config = config.Value;
            _database = access.Connect();
        }
		
        public async Task<SiteConfig> Config()
        {
            var conn = _database.GetCollection<SiteConfig>("Config");
            var temp = await conn.Find(_=>true).FirstOrDefaultAsync();

            //return the modified copy of Target			
            Merge(_config, temp);

            return _config;
        }

        public static SiteConfig Merge(SiteConfig target, SiteConfig source)
        {
            typeof(SiteConfig)
                .GetProperties()
                .Select(x => new KeyValuePair<PropertyInfo, object>(x, x.GetValue(source, null)))
                .Where(x => x.Value != null).ToList()
                .ForEach(x => x.Key.SetValue(target, x.Value, null));

            return target;  
        }
    }
}