using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenLi.Api.Domain.Models.RandomUserProperties
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Gender
	{
		Male,
		Female
	}
}
