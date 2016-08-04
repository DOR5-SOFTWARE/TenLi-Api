using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenLi.Api.Domain.Models.RandomUserProperties
{
	public class Firstname
	{
		public string HebValue { get; set; }
		public string EngValue { get; set; }

		[JsonIgnore]
		public Gender Gender { get; set; }
	}
}
