namespace TenLi.Api.Domain.Models.RandomUserProperties
{
    public class Address
    {
        public City City { get; set; }
        public Street Street { get; set; }
        public int HouseNumber { get; set; }
    }

    public class City : MultyLanguageStringEntity{}

    public class Street : MultyLanguageStringEntity{}
}