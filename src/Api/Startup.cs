namespace Api
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Localization;
	using Microsoft.AspNetCore.Mvc.Razor;

	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;

	using System.Globalization;

	using Api.Connections;
	using Api.Filters;
	using Api.Repositories;

	using Library.Config;
	using Library.Models;

	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", true, true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
				.AddJsonFile("mongodb.json")
				.AddJsonFile("config.json")
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

			// Add framework services.
			services.AddMvc()
				.AddViewLocalization(
				LanguageViewLocationExpanderFormat.Suffix,
				opts => { opts.ResourcesPath = "Resources"; })
				.AddDataAnnotationsLocalization();

			// RKLANKE add MongoDB to site
			services.Configure<Database>(Configuration);
			services.Configure<SiteConfig>(Configuration);
			services.Configure<RequestLocalizationOptions>(
				options =>
				{
					var supportedCultures = new[]
					{
						new CultureInfo("en-US"),
						new CultureInfo("en"),
						new CultureInfo("nl-NL"),
						new CultureInfo("nl"),
					};

					options.DefaultRequestCulture = new RequestCulture("en-US");
					// Formatting numbers, dates, etc.
					options.SupportedCultures = supportedCultures;
					// UI strings that we have localized.
					options.SupportedUICultures = supportedCultures;
				}
			);

			services.AddTransient<IArticleRepository, ArticleRepository>();
			services.AddTransient<INavigationRepository, NavigationRepository>();
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IBoxRepository, BoxRepository>();
			services.AddTransient<IConfigRepository, ConfigRepository>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddScoped<LanguageActionFilter>();

			services.AddTransient<IDatabaseConnection, DatabaseConnection>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization(options.Value);

			app.UseMvc();
		}
	}
}
