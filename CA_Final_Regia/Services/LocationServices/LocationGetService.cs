using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.DTOs;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;

namespace CA_Final_Regia.Services.LocationServices
{
    public class LocationGetService(ILocationRepository locationRepository) : ILocationGetService
    {
        private readonly ILocationRepository _locationRepository = locationRepository;
        public async Task<ResponseDto<Location>> GetLocationAsync(Guid accountId)
        {
            try
            {
                var location = await _locationRepository.GetLocationAsync(accountId);
                if (location == null)
                {
                    return new ResponseDto<Location>(false, "Location not found.", ResponseDto<Location>.Status.Not_Found);
                }
                return new ResponseDto<Location>(true, location, ResponseDto<Location>.Status.Ok);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
