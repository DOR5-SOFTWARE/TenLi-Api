using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TenLi.Api.Domain.Models;
using TenLi.Api.Domain.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TenLi.Api.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RandomUsersController : Controller
    {
        [HttpGet]
        public RandomUser GetRandomUser()
        {
            var randomUsersGenerator = new RandomUsersGenerator();
            return randomUsersGenerator.GenerateRandomUser();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
