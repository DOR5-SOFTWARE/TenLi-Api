using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenLi.Api.DataAccess.Mongo;

namespace TenLi.Api.Domain.Models.RandomUserProperties
{
	public class Lastname : Entity
	{
		public string HebValue { get; set; }
		public string EngValue { get; set; }
	}
}
