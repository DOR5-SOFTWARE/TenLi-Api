using Newtonsoft.Json;
using TenLi.Api.DataAccess.Mongo;

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
