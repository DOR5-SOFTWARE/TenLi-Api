using MongoDB.Bson.Serialization.Attributes;
using TenLi.Api.DataAccess.Mongo;

namespace TenLi.Api.Domain.Models.RandomUserProperties
{
	public class Address : Entity
	{
		public string City { get; set; }
		public string Street { get; set; }

		[BsonIgnore]
		public int HouseNumber { get; set; }

		[BsonIgnore]
		public string AddressString
		{
			get
			{
				return string.Format("{0} {1}, {2}", Street, HouseNumber, City);
			}
		}
	}
}