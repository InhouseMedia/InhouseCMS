namespace Api.Repositories
{
	using System.Threading.Tasks;

	using Microsoft.Extensions.Options;
	using MongoDB.Driver;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
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

			var jsonDefaultConfig = JsonConvert.SerializeObject(_config);
			var jsonSiteConfig = JsonConvert.SerializeObject(result, Formatting.None,
							new JsonSerializerSettings
							{
								NullValueHandling = NullValueHandling.Ignore
							});

			// Parse Json file into JObject to prepare for mergin
			var defaultJsonObject = JObject.Parse(jsonDefaultConfig);
			var siteJsonObject = JObject.Parse(jsonSiteConfig);

			defaultJsonObject.Merge(siteJsonObject);

			// Place Merged Json object into Config Class
			return defaultJsonObject.ToObject<SiteConfig>();
		}
	}
}