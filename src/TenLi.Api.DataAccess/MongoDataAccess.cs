using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB;
using Microsoft.Extensions.Configuration;

namespace TenLi.Api.DataAccess
{
	public interface IMongoDataAccess
	{
		string ConnectionString { get; }
	}

    public class MongoDataAccess : IMongoDataAccess
	{
		private readonly string _mongoConnectionString;
		private readonly IConfigurationRoot _configuration;

		public MongoDataAccess(IConfigurationRoot configuration)
        {
			_configuration = configuration;
			_mongoConnectionString = _configuration["MongoDB:ConnectionString"];
		}

		public string ConnectionString
		{
			get { return _mongoConnectionString; }
		}
    }
}
