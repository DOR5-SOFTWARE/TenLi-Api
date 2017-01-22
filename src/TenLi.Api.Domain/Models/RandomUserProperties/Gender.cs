using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TenLi.Api.Domain.Models.RandomUserProperties
{
    [JsonConverter(typeof(StringEnumConverter))]
	public enum Gender
	{
		Any,
		Male,
		Female
	}
}
