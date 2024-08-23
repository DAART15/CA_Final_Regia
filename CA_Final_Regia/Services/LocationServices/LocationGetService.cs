using CA_Final_Regia.DTOs;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;

namespace CA_Final_Regia.Services.LocationServices
{
    public class LocationGetService(ILocationRepository locationRepository) : ILocationGetService
    {
        private readonly ILocationRepository _locationRepository = locationRepository;
        public async Task<ResponseDto<LocationDto>> GetLocationAsync(Guid accountId)
        {
            try
            {
                var location = await _locationRepository.GetLocationAsync(accountId);
                if (location == null)
                {
                    return new ResponseDto<LocationDto>(false, "Location not found.", ResponseDto<LocationDto>.Status.Not_Found);
                }
                LocationDto locationDto = new LocationDto
                {
                    City = location.City,
                    Street = location.Street,
                    HouseNr = location.HouseNr,
                    ApartmentNr = location.ApartmentNr
                };
                return new ResponseDto<LocationDto>(true, locationDto, ResponseDto<LocationDto>.Status.Ok);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
