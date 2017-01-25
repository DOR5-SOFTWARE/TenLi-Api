using TenLi.Api.Domain.Models.RandomUserProperties;

namespace TenLi.Api.Domain.Models
{
	public class RandomUser
	{
		public Firstname Firstname { get; set; }
		public Lastname Lastname { get; set; }
		public Image Image { get; set; }
		public Gender Gender { get; set; }
		public Profession Profession { get; set; }
		public string PhoneNumber { get; set; }

		public Address Address { get; set; }

		public string Username {
			get
			{
				var firstnameValue = Firstname.IsNullOrEmpty(false, true) ? "firstname" : Firstname.EngValue;
				var lastnameValue = Lastname.IsNullOrEmpty(false, true) ? "lastname" : Lastname.EngValue;
				return firstnameValue + "." + lastnameValue;
			}
		}

		public string Email
		{
			get
			{
				return Username + "@ExampleDomain.co.il";
			}
		}
	}
}
