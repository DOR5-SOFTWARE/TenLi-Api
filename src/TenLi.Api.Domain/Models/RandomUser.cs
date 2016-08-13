using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenLi.Api.Domain.Models.RandomUserProperties;

namespace TenLi.Api.Domain.Models
{
	public class RandomUser
	{
		public Firstname Firstname { get; set; }
		public Lastname Lastname { get; set; }
		public Image Image { get; set; }

		public Gender Gender { get; set; }

		public string Email
		{
			get
			{
				var firstnameValue = (Firstname != null && !string.IsNullOrEmpty(Firstname.EngValue)) ? Firstname.EngValue : "firstname";
				var lastnameValue = (Lastname != null && !string.IsNullOrEmpty(Lastname.EngValue)) ? Lastname.EngValue : "lastname";
				return firstnameValue + "." + lastnameValue + "@ExampleDomain.co.il";
			}
		}
	}
}
