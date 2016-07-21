using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TenLi.Api.Domain.Models;
using TenLi.Api.Domain.Services;

namespace TenLi.Api.Web.Controllers
{
	[Route("api/V1/[controller]/[action]")]
	public class RandomUsersController : Controller
	{
		[HttpGet]
		public RandomUser GetRandomUser()
		{
			try
			{
				var randomUsersGenerator = new RandomUsersGenerator();
				return randomUsersGenerator.GenerateRandomUser();
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
