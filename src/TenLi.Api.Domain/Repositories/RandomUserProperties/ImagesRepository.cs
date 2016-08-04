using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		private const string MALE_IMAGES_CACHE_KEY = "MaleImages";
		private const string FEMALE_IMAGES_CACHE_KEY = "FemaleImages";

		public ImagesRepository(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
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
			var maleImages = new Image[] {
				new Image
				{
					Large = "https://s3.amazonaws.com/uifaces/faces/twitter/rem/128.jpg",
					Medium = "https://s3.amazonaws.com/uifaces/faces/twitter/rem/73.jpg",
					Small = "https://s3.amazonaws.com/uifaces/faces/twitter/rem/48.jpg",
					Gender = Gender.Male
				},
				new Image
				{
					Large = "https://s3.amazonaws.com/uifaces/faces/twitter/ripplemdk/128.jpg",
					Medium = "https://s3.amazonaws.com/uifaces/faces/twitter/ripplemdk/73.jpg",
					Small = "https://s3.amazonaws.com/uifaces/faces/twitter/ripplemdk/48.jpg",
					Gender = Gender.Male
				},

				new Image
				{
					Large = "https://s3.amazonaws.com/uifaces/faces/twitter/ok/128.jpg",
					Medium = "https://s3.amazonaws.com/uifaces/faces/twitter/ok/73.jpg",
					Small = "https://s3.amazonaws.com/uifaces/faces/twitter/ok/48.jpg",
					Gender = Gender.Male
				}
			};

			return maleImages.ToList();
		}

		private List<Image> InitializeFemaleImages(ICacheEntry arg)
		{
			var femaleImages = new Image[]
			{
				new Image
				{
					Large = "https://s3.amazonaws.com/uifaces/faces/twitter/jina/128.jpg",
					Medium = "https://s3.amazonaws.com/uifaces/faces/twitter/jina/73.jpg",
					Small = "https://s3.amazonaws.com/uifaces/faces/twitter/jina/48.jpg",
					Gender = Gender.Female
				},
				new Image
				{
					Large = "https://s3.amazonaws.com/uifaces/faces/twitter/nuraika/128.jpg",
					Medium = "https://s3.amazonaws.com/uifaces/faces/twitter/nuraika/73.jpg",
					Small = "https://s3.amazonaws.com/uifaces/faces/twitter/nuraika/48.jpg",
					Gender = Gender.Female
				}
			};

			return femaleImages.ToList();
		}
	}
}
