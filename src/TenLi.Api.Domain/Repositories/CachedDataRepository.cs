using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using TenLi.Api.DataAccess.Mongo;

namespace TenLi.Api.Domain.Repositories
{
    public interface ICachedDataRepository<T> : IEnumerable<T>
    {
        T this[int index] { get; }
        List<T> DataObjects { get; }
    }

    public class CachedDataRepository<T> : ICachedDataRepository<T>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IMongoRepository<T> _mongoRepository;

        private string cacheKey = typeof(T).Name;

        public CachedDataRepository(IMemoryCache memoryCache, IMongoRepository<T> mongoRepository)
        {
            _memoryCache = memoryCache;
            _mongoRepository = mongoRepository;
        }

        public List<T> DataObjects
        {
            get
            {
                return _memoryCache.GetOrCreate(cacheKey, InitializeCollectionFromDb);
            }
        }

        private List<T> InitializeCollectionFromDb(ICacheEntry arg)
        {
            var dataObjects = _mongoRepository.GetList(x => true);

            return dataObjects;
        }

        public T this[int index]
        {
            get { return DataObjects[index]; }
        }
        public IEnumerator<T> GetEnumerator()
        {
            return DataObjects.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
