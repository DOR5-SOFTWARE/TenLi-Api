using Newtonsoft.Json;

namespace TenLi.Api.Domain.Models.RandomUserProperties
{
    public class Firstname : MultyLanguageStringEntity
	{
		[JsonIgnore]
		public Gender Gender { get; set; }
	}
}
