namespace Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.Routing;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using System.Globalization;
	using System.Linq;
	using System.Threading.Tasks;

	using Library.Models;

    using Web.Connections;
    using Web.Filters;
    using Web.Repositories;

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
						var config = context.RequestServices.GetService<ConfigRepository>();
						var culture = config.GetConfig().Language.Locale.FirstOrDefault();

						CultureInfo.CurrentCulture = new CultureInfo(culture);
						CultureInfo.CurrentUICulture = new CultureInfo(culture);

						CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
						CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(culture);

						return await Task.FromResult(new ProviderCultureResult(culture));
					}));

				}
            );

			services.AddSingleton<IArticleRepository, ArticleRepository>();
			services.AddSingleton<IBoxRepository, BoxRepository>();
            services.AddSingleton<INavigationRepository, NavigationRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<LocalizationActionFilter>();

			services.AddTransient<ApiConnection>();
			services.AddTransient<ConfigRepository>();
			services.AddTransient<IRouteConnection, RouteConnection>();
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
				//routes.Routes.Add(routes.ServiceProvider.GetService<IRouteConnection>());

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
/*
                routes.MapRoute( 
                    name: "url", 
                    template: "{*path}",
                    defaults: new {controller = "Article", action = "Index"});
*/
                routes.MapGet("{*path}", context =>
                    {
                        var article = context.RequestServices.GetService<IArticleRepository>();
						var navigation = context.RequestServices.GetService<INavigationRepository>();

						var path = context.Request.Path.Value ?? "/";
                        var navItem = navigation.GetNavigationItem(path);
	                    var articleId = navItem?.ArticleId ?? "012345678901234567890123"; // fake articleId needs to be 24 chars long

	                    var articleItem = article.GetPage(articleId);

	                   


	                    return context.Response.WriteAsync($"Hi, {articleItem.Result.MetaTitle}!");
                    }
                );
            });
        }
    }
}
