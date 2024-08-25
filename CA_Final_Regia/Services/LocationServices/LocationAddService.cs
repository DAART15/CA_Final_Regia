using CA_Final_Regia.Interfaces;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.DTOs;
namespace CA_Final_Regia.Services.LocationServices
{
    public class LocationAddService(ILocationRepository locationRepository, IDtoValidation<LocationDto> dtoValidation) : ILocationAddService
    {
        public async Task<ResponseDto<Location>> AddLocationAsync(LocationDto locationDto, Guid accountId)
        {
            try
            {
                var validation = dtoValidation.DtoKeyValueValidation(locationDto);
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
                var response = await locationRepository.CreateLocationAsync(location);
                if (response == null)
                {
                    return new ResponseDto<Location>(false, "Location not added", ResponseDto<Location>.Status.Internal_Server_Error);
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
