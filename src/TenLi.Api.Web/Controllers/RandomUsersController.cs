using System;
using Microsoft.AspNetCore.Mvc;
using TenLi.Api.Domain.Models;
using TenLi.Api.Domain.Models.RandomUserProperties;
using TenLi.Api.Domain.Services;

namespace TenLi.Api.Web.Controllers
{
    [Route("api/V1/[controller]/[action]")]
	public class RandomUsersController : Controller
	{
		private readonly IRandomUsersGenerator _randomUsersGenerator;

		public RandomUsersController(IRandomUsersGenerator randomUsersGenerator)
		{
			_randomUsersGenerator = randomUsersGenerator;
		}

		[HttpGet]
		public RandomUser GetRandomUser([FromQuery] Gender gender = Gender.Any)
		{
			try
			{
				return _randomUsersGenerator.GenerateRandomUser(gender);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
