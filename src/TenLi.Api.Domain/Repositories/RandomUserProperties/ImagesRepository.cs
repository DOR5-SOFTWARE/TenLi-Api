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
	public interface IImagesRepository
	{
		List<Image> MaleImages { get; }
		List<Image> FemaleImages { get; }
		List<Image> Images(Gender gender);
	}

	public class ImagesRepository : IImagesRepository
	{
		private readonly IMemoryCache _memoryCache;
		private readonly IMongoRepository<Image> _imagesMongoRepository;

		private const string MALE_IMAGES_CACHE_KEY = "MaleImages";
		private const string FEMALE_IMAGES_CACHE_KEY = "FemaleImages";

		public ImagesRepository(IMemoryCache memoryCache, IMongoRepository<Image> imagesMongoRepository)
		{
			_memoryCache = memoryCache;
			_imagesMongoRepository = imagesMongoRepository;
		}

		public List<Image> MaleImages
		{
			get
			{
				return _memoryCache.GetOrCreate(MALE_IMAGES_CACHE_KEY, InitializeMaleImages);
			}
		}

		public List<Image> FemaleImages
		{
			get
			{
				return _memoryCache.GetOrCreate(FEMALE_IMAGES_CACHE_KEY, InitializeFemaleImages);
			}
		}

		public List<Image> Images(Gender gender)
		{
			return gender == Gender.Male ? MaleImages : FemaleImages;
		}

		private List<Image> InitializeMaleImages(ICacheEntry arg)
		{
			var maleImages = _imagesMongoRepository.GetList(x => x.Gender == Gender.Male);

			return maleImages;
		}

		private List<Image> InitializeFemaleImages(ICacheEntry arg)
		{
			var femaleImages = _imagesMongoRepository.GetList(x => x.Gender == Gender.Female);

			return femaleImages;
		}
	}
}
