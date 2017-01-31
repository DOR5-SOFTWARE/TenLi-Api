using System;
using System.Linq;
using TenLi.Api.Domain.Models;
using TenLi.Api.Domain.Models.RandomUserProperties;
using TenLi.Api.Domain.Repositories;
using System.Text;

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
			Func<Firstname, bool> filterFirstnamesByGender = (Firstname f) => (gender == Gender.Any ? true : f.Gender == gender);
			var filteredFirstnames = _firstnames.Where(filterFirstnamesByGender).ToList();
			var firstName = filteredFirstnames.ElementAt(_random.Next(0, filteredFirstnames.Count()));

			var lastName = _lastnames[_random.Next(0, _lastnames.Count())];

			var filteredImages = _images.Where(f => f.Gender == firstName.Gender);
			var image = filteredImages.ElementAt(_random.Next(0, filteredImages.Count()));

			var profession = new Profession
			{
				HebValue = "מנהל פרוייקט",
				EngValue = "Project Manager"
			};

			var address = _randomAddressGenerator.GenerateRandomAddress();

			var phoneNumber = GenerateRandomPhoneNumber();

			return new RandomUser
			{
				Firstname = firstName,
				Lastname = lastName,
				Image = image,
				Gender = firstName.Gender,
				Profession = profession,
				Address = address,
				PhoneNumber = phoneNumber
			};
		}

		private string GenerateRandomPhoneNumber()
		{
			var phoneStringBuilder = new StringBuilder();

			phoneStringBuilder.Append("05");
			phoneStringBuilder.Append(_random.Next(2, 7));

			while (phoneStringBuilder.Length < 10)
			{
				phoneStringBuilder.Append(_random.Next(0, 9));
			}

			return phoneStringBuilder.ToString();
		}
	}
}
