using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using TenLi.Api.Domain.Services;
using TenLi.Api.Domain.Repositories;
using TenLi.Api.DataAccess.Mongo;
using TenLi.Api.Domain.Models.RandomUserProperties;

namespace TenLi.Api.Web
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var pathToDoc = Configuration["Swagger:Path"];

			services.AddMvc()
				.AddJsonOptions(opt =>
				{
					opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

					var resolver = opt.SerializerSettings.ContractResolver;

					opt.SerializerSettings.Formatting = Formatting.Indented;
				});

			services.AddMemoryCache();

			services.AddSwaggerGen();

			services.ConfigureSwaggerGen(options =>
			{
				options.SingleApiVersion(new Info
				{
					Version = "v1",
					Title = "Get Random Hebrew User",
					Description = "A simple api to get random users with Hebrew names",
					TermsOfService = "None"
				});
				//options.IncludeXmlComments(pathToDoc);
				options.DescribeAllEnumsAsStrings();
			});

			services.AddSingleton(typeof(IConfigurationRoot), Configuration);

			services.AddSingleton<IMongoDatabaseProvider, MongoDatabaseProvider>();

			services.AddTransient<IMongoRepository<Firstname>, MongoRepository<Firstname>>();
			services.AddTransient<IMongoRepository<Lastname>, MongoRepository<Lastname>>();
			services.AddTransient<IMongoRepository<Image>, MongoRepository<Image>>();
			services.AddTransient<IMongoRepository<Street>, MongoRepository<Street>>();
			services.AddTransient<IMongoRepository<City>, MongoRepository<City>>();
			services.AddTransient<IMongoRepository<Profession>, MongoRepository<Profession>>();			

			services.AddSingleton<ICachedDataRepository<Firstname>, CachedDataRepository<Firstname>>();
			services.AddSingleton<ICachedDataRepository<Lastname>, CachedDataRepository<Lastname>>();
			services.AddSingleton<ICachedDataRepository<Image>, CachedDataRepository<Image>>();
			services.AddSingleton<ICachedDataRepository<Street>, CachedDataRepository<Street>>();
			services.AddSingleton<ICachedDataRepository<City>, CachedDataRepository<City>>();
			services.AddSingleton<ICachedDataRepository<Profession>, CachedDataRepository<Profession>>();

			services.AddSingleton<IRandomAddressGenerator, RandomAddressGenerator>();
			services.AddSingleton<IRandomUsersGenerator, RandomUsersGenerator>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseMvc();

			app.UseSwagger();
			app.UseSwaggerUi();
		}
	}
}
