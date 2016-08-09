using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;
using System.IO;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using TenLi.Api.Domain.Services;
using Microsoft.Extensions.Caching.Memory;
using TenLi.Api.Domain.Repositories.RandomUserProperties;
using TenLi.Api.DataAccess;

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

			services.AddMvc().AddJsonOptions(opt =>
			{
				var resolver = opt.SerializerSettings.ContractResolver;

				if (resolver != null)
				{
					var res = resolver as DefaultContractResolver;
					res.NamingStrategy = null;
				}

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
			
			services.AddSingleton<IMongoDataAccess, MongoDataAccess>();
			services.AddSingleton<IFirstnamesRepository, FirstnamesRepository>();
			services.AddSingleton<ILastnamesRepository, LastnamesRepository>();
			services.AddSingleton<IImagesRepository, ImagesRepository>();

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
