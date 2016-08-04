using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenLi.Api.Domain.Models;
using TenLi.Api.Domain.Models.RandomUserProperties;
using TenLi.Api.Domain.Repositories.RandomUserProperties;

namespace TenLi.Api.Domain.Services
{
	public interface IRandomUsersGenerator
	{
		RandomUser GenerateRandomUser();
	}

	public class RandomUsersGenerator : IRandomUsersGenerator
	{
		private readonly IFirstnamesRepository _firstnames;
		private readonly ILastnamesRepository _lastnames;
		private readonly IImagesRepository _images;

		private readonly Random _random;

		public RandomUsersGenerator(IFirstnamesRepository firstnamesRepository, ILastnamesRepository lastnamesRepository, IImagesRepository imagesRepository)
		{
			_firstnames = firstnamesRepository;
			_lastnames = lastnamesRepository;
			_images = imagesRepository;

			_random = new Random();
		}

		public RandomUser GenerateRandomUser()
		{
			var firstName = _firstnames.Firstnames[_random.Next(0, _firstnames.Firstnames.Count)];
			var lastName = _lastnames.Lastnames[_random.Next(0, _lastnames.Lastnames.Count)];
			var image = _images.Images(firstName.Gender)[_random.Next(0, _images.Images(firstName.Gender).Count)];

			return new RandomUser
			{
				Firstname = firstName,
				Lastname = lastName,
				Image = image,
				Gender = firstName.Gender
			};
		}
	}
}
