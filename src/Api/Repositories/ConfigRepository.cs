namespace Api.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    using Library.Config;
    using Api.Connections;

    public interface IConfigRepository
    {
        Task<SiteConfig> Config();
    }

    public class ConfigRepository : ConnectionRepository, IConfigRepository
    {
        private readonly SiteConfig _config;

        public ConfigRepository(DatabaseConnection database, IOptions<SiteConfig> config) : base(database)
        {
            _config = config.Value;
        }

        public async Task<SiteConfig> Config()
        {
            var conn = _database.GetCollection<SiteConfig>("Config");
            var result = await conn.Find(_ => true).FirstOrDefaultAsync();

            //return the modified copy of Target			
            Merge(_config, result);

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