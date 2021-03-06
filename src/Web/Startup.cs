namespace Web
{
	using Library.Connections;
	using Library.Models;
	using Library.Repositories;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Localization;
	using Microsoft.AspNetCore.Mvc.Razor;
	using Microsoft.AspNetCore.Routing;

	using Microsoft.Extensions.Caching.Memory;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;

	using System;
	using System.Globalization;
	using System.Linq;
	using System.Threading.Tasks;

	using Web.Filters;
	using Web.Connections;

	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", true, true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
				.AddJsonFile("api.json")
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services
				.AddLocalization(options => { options.ResourcesPath = "Resources"; })
				.AddMvc()
				.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
				.AddDataAnnotationsLocalization();

			services.Configure<Api>(Configuration);
			services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
			services.Configure<RequestLocalizationOptions>(
				options =>
				{
					var supportedCultures = new[]
					{
						new CultureInfo("en-US"),
						new CultureInfo("en"),
						new CultureInfo("nl-NL"),
						new CultureInfo("nl")
					};

					options.DefaultRequestCulture = new RequestCulture("en-US");
					options.SupportedCultures = supportedCultures;
					options.SupportedUICultures = supportedCultures;

					// Set Current Culture for views
					options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
					{
						var cookie = context.Request.Cookies["locale"]; //context.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

						var config = context.RequestServices.GetService<ConfigRepository>();
						var defaultCulture = config.GetConfig().Language.Locale.FirstOrDefault();
						var defaultCookieCulture = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(defaultCulture));
						var tmpCulture = cookie ?? defaultCookieCulture;

						var cultures = CookieRequestCultureProvider.ParseCookieValue(tmpCulture);

						var culture = cultures.Cultures.FirstOrDefault();
						var uICulture = cultures.UICultures.FirstOrDefault();

						CultureInfo.CurrentCulture = new CultureInfo(culture);
						CultureInfo.CurrentUICulture = new CultureInfo(uICulture);

						CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
						CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(uICulture);

						return await Task.FromResult(new ProviderCultureResult(culture));
					}));

				}
			);

			services.AddTransient<IArticleRepository, ArticleRepository>();
			services.AddTransient<IBoxRepository, BoxRepository>();
			services.AddTransient<INavigationRepository, NavigationRepository>();
			services.AddTransient<IProductRepository, ProductRepository>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddScoped<LocalizationActionFilter>();

			services.AddTransient<ApiConnection>();
			services.AddTransient<ConfigRepository>();
			//services.AddTransient<IRouteConnection, RouteConnection>();
			//services.AddTransient<IMemoryCache, MemoryCache>();
			//services.AddTransient<IRouter, Route>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization(options.Value);

			app.UseMvc(routes =>
			{
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");

				//routes.Routes.Add(routes.ServiceProvider.GetService<IRouteConnection>());
				routes.Routes.Add( new RouteConnection(
					routes.ServiceProvider.GetRequiredService<IMemoryCache>(),
					routes.DefaultHandler));
			});
		}
	}
}
