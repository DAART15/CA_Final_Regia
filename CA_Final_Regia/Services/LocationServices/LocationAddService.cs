using CA_Final_Regia.Interfaces;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.DTOs;
using CA_Final_Regia.Properties.ActionFilters;
namespace CA_Final_Regia.Services.LocationServices
{
    public class LocationAddService(ILocationRepository locationRepository) : ILocationAddService
    {
        private readonly ILocationRepository _locationRepository = locationRepository;
        public async Task<ResponseDto<Location>> AddLocationAsync(LocationDto locationDto, Guid accountId)
        {
            try
            {
                var validation = locationDto.ValidateLocationDto();
                if (!validation.IsSuccess)
                {
                    return new ResponseDto<Location>(false, validation.Message, ResponseDto<Location>.Status.Bad_Request);
                }
                Location location = new Location
                {
                    AccountId = accountId,
                    City = locationDto.City,
                    Street = locationDto.Street,
                    HouseNr = locationDto.HouseNr,
                    ApartmentNr = locationDto.ApartmentNr
                };
                var response = await _locationRepository.CreateLocationAsync(location);
                if (response == null)
                {
                    return new ResponseDto<Location>(false, "Location not added", ResponseDto<Location>.Status.Not_Found);
                }
                return new ResponseDto<Location>(true, "Location added", ResponseDto<Location>.Status.Created);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
