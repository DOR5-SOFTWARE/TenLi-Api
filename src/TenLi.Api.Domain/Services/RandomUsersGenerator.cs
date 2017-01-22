using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenLi.Api.Domain.Models;
using TenLi.Api.Domain.Models.RandomUserProperties;
using TenLi.Api.Domain.Repositories;

namespace TenLi.Api.Domain.Services
{
	public interface IRandomUsersGenerator
	{
		RandomUser GenerateRandomUser(Gender gender);
	}

	public class RandomUsersGenerator : IRandomUsersGenerator
	{
		private readonly ICachedDataRepository<Firstname> _firstnames;
		private readonly ICachedDataRepository<Lastname> _lastnames;
		private readonly ICachedDataRepository<Image> _images;
		private readonly ICachedDataRepository<Profession> _professions;
		private readonly IRandomAddressGenerator _randomAddressGenerator;

		private readonly Random _random;

		public RandomUsersGenerator(
			ICachedDataRepository<Firstname> firstnamesRepository, 
			ICachedDataRepository<Lastname> lastnamesRepository, 
			ICachedDataRepository<Image> imagesRepository,
			ICachedDataRepository<Profession> professionsRepository,
			IRandomAddressGenerator randomAddressGenerator
			)
		{
			_firstnames = firstnamesRepository;
			_lastnames = lastnamesRepository;
			_images = imagesRepository;
			_professions = professionsRepository;
			_randomAddressGenerator = randomAddressGenerator;

			_random = new Random();
		}

		public RandomUser GenerateRandomUser(Gender gender)
		{
			List<Firstname> firstnames;
			List<Image> images;

			if(gender == Gender.Any){
				firstnames = _firstnames.ToList();
				images = _images.ToList();
			} else{
				firstnames = _firstnames.Where(f => f.Gender == gender).ToList();
				images = _images.Where(f => f.Gender == gender).ToList();
			}

			var firstName = firstnames[_random.Next(0, firstnames.Count())];
			var lastName = _lastnames[_random.Next(0, _lastnames.Count())];
			var image = images[_random.Next(0, images.Count())];
			
			
			//var profession = _professions[_random.Next(0, _professions.Count())];
			var profession = new Profession{
				HebValue = "מנהל פרוייקט",
				EngValue = "Project Manager"
			};
			
			var address = _randomAddressGenerator.GenerateRandomAddress();

			return new RandomUser
			{
				Firstname = firstName,
				Lastname = lastName,
				Image = image,
				Gender = firstName.Gender,
				Profession = profession,
				Address = address
			};
		}
	}
}
