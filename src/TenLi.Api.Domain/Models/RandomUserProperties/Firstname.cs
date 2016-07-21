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

		public bool IsMale { get; set; }
		public bool IsFemale { get; set; }
	}
}
