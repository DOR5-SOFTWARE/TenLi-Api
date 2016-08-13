using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TenLi.Api.Domain.Models;
using TenLi.Api.Domain.Services;
using TenLi.Api.DataAccess;

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
		public RandomUser GetRandomUser()
		{
			try
			{
				return _randomUsersGenerator.GenerateRandomUser();
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
