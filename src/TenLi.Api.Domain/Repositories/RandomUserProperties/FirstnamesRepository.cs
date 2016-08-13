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
	public interface IFirstnamesRepository
	{
		List<Firstname> Firstnames { get; }
	}

	public class FirstnamesRepository : IFirstnamesRepository
	{
		private readonly IMemoryCache _memoryCache;
		private readonly IMongoRepository<Firstname> _firstnamesMongoRepository;

		private const string FIRSTNAMES_CACHE_KEY = "Firstnames";

		public FirstnamesRepository(IMemoryCache memoryCache, IMongoRepository<Firstname> firstnamesMongoRepository)
		{
			_memoryCache = memoryCache;
			_firstnamesMongoRepository = firstnamesMongoRepository;
		}

		public List<Firstname> Firstnames
		{
			get
			{
				return _memoryCache.GetOrCreate(FIRSTNAMES_CACHE_KEY, InitializeFirstnames);
			}
		}

		private List<Firstname> InitializeFirstnames(ICacheEntry arg)
		{
			var firstnames = _firstnamesMongoRepository.GetList(x => true);

			return firstnames;
		}
	}
}
