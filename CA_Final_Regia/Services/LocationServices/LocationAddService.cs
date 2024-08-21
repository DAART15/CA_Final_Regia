using CA_Final_Regia.Interfaces;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.DTOs;
namespace CA_Final_Regia.Services.LocationServices
{
    public class LocationAddService(ILocationRepository locationRepository) : ILocationAddService
    {
        private readonly ILocationRepository _locationRepository = locationRepository;
        public async Task<Location> AddLocationAsync(LocationDto locationDto, Guid accountId)
        {
            try
            {
                Location location = new Location
                {
                    AccountId = accountId,
                    City = locationDto.City,
                    Street = locationDto.Street,
                    HouseNr = locationDto.HouseNr,
                    ApartmentNr = locationDto.ApartmentNr
                };
                return await _locationRepository.CreateLocationAsync(location);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
