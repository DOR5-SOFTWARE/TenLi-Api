using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		private const string FIRSTNAMES_CACHE_KEY = "Firstnames";

		public FirstnamesRepository(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
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
			var firstnames = new Firstname[] {
					new Firstname
					{
						EngValue = "Anton",
						HebValue = "אנטון",
						Gender = Gender.Male
					},
					new Firstname
					{
						EngValue = "Valeria",
						HebValue = "ולריה",
						Gender = Gender.Female
					},
					new Firstname
					{
						EngValue = "Eli",
						HebValue = "עילאי",
						Gender = Gender.Male
					},
					new Firstname
					{
						EngValue = "Eithan",
						HebValue = "איתן",
						Gender = Gender.Male
					}
			};

			return firstnames.ToList();
		}
	}
}
