using System;

namespace TenLi.Api.Domain.Models
{
    public class UnexpectedExceptionResult
    {
		public string ExceptionMessage { get; set; }
		public string InnerExceptionMessage { get; set; }
		public Tuple<int, string>[] ExtraData { get; set; }
	}
}
