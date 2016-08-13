using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenLi.Api.DataAccess.Mongo;

namespace TenLi.Api.Domain.Models.RandomUserProperties
{
	public class Firstname : Entity
	{
		public string HebValue { get; set; }
		public string EngValue { get; set; }

		[JsonIgnore]
		public Gender Gender { get; set; }
	}
}
