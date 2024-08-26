using CA_Final_Regia.Domain.Entities;
using CA_Final_Regia.Domain.Interfaces.Repository;
using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
namespace CA_Final_Regia.Services.Services.LocationServices
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
                await locationRepository.CreateLocationAsync(location);
                return new ResponseDto<Location>(true, "Location added", ResponseDto<Location>.Status.Created);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
