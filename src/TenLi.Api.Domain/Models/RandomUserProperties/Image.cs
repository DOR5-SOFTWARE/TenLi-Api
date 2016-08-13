using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenLi.Api.DataAccess.Mongo;
using TenLi.Api.Domain.Models.RandomUserProperties;

namespace TenLi.Api.Domain.Models.RandomUserProperties
{
	public class Image : Entity
	{
		public string Small { get; set; }
		public string Medium { get; set; }
		public string Large { get; set; }

		[JsonIgnore]
		public Gender Gender { get; set; }
	}
}
