using System;
using System.Linq;
using TenLi.Api.Domain.Models.RandomUserProperties;
using TenLi.Api.Domain.Repositories;

namespace TenLi.Api.Domain.Services
{
    public interface IRandomAddressGenerator{
        Address GenerateRandomAddress();
    }
    public class RandomAddressGenerator : IRandomAddressGenerator
    {
        private readonly ICachedDataRepository<Address> _addresses;
        private readonly Random _random;

        public RandomAddressGenerator(ICachedDataRepository<Address> addressesRepository){
            _addresses = addressesRepository;
            _random = new Random();
        }

        public Address GenerateRandomAddress()
        {
            var address = _addresses[_random.Next(0, _addresses.Count())];
            address.HouseNumber = _random.Next(5, 40);

            return address;
        }
    }
}