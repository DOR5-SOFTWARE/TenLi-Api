using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenLi.Api.DataAccess.Mongo;
using TenLi.Api.Domain.Models.RandomUserProperties;

namespace TenLi.Api.Domain.Repositories.RandomUserProperties
{
	public interface ILastnamesRepository
	{
		List<Lastname> Lastnames { get; }
	}

	public class LastnamesRepository : ILastnamesRepository
	{
		private readonly IMemoryCache _memoryCache;
		private readonly IMongoRepository<Lastname> _lastnamesMongoRepository;

		private const string LASTNAMES_CACHE_KEY = "Lastnames";

		public LastnamesRepository(IMemoryCache memoryCache, IMongoRepository<Lastname> lastnamesMongoRepository)
		{
			_memoryCache = memoryCache;
			_lastnamesMongoRepository = lastnamesMongoRepository;
		}

		public List<Lastname> Lastnames
		{
			get
			{
				return _memoryCache.GetOrCreate(LASTNAMES_CACHE_KEY, InitializeLastnames);
			}
		}

		private List<Lastname> InitializeLastnames(ICacheEntry arg)
		{
			var lastnames = _lastnamesMongoRepository.GetList(x => true);

			return lastnames;
		}
	}
}
