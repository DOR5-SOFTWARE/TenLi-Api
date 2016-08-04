using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		private const string LASTNAMES_CACHE_KEY = "Lastnames";

		public LastnamesRepository(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
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
			var lastnames = new Lastname[] {
				new Lastname
				{
					EngValue = "Gantman",
					HebValue = "גנטמן"
				},
				new Lastname
				{
					EngValue = "Golan",
					HebValue = "גולן"
				},
				new Lastname
				{
					EngValue = "Peretz",
					HebValue = "פרץ"
				},
				new Lastname
				{
					EngValue = "Fischer",
					HebValue = "פישר"
				}
			};

			return lastnames.ToList();
		}
	}
}
