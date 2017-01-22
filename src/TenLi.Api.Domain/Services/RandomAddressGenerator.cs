using System;
using TenLi.Api.Domain.Models.RandomUserProperties;

namespace TenLi.Api.Domain.Services
{
    public interface IRandomAddressGenerator{
        Address GenerateRandomAddress();
    }
    public class RandomAddressGenerator : IRandomAddressGenerator
    {
        public Address GenerateRandomAddress()
        {
            return new Address{
                City = new City{
                    HebValue = "תל אביב",
                    EngValue = "Tel Aviv"
                },
                Street = new Street{
                    HebValue = "אבן גבירול",
                    EngValue = "Even Gvirol"
                },
                HouseNumber = 50
            };
        }
    }
}