using CA_Final_Regia.Domain.Entities;
using CA_Final_Regia.Domain.Interfaces.Repository;
using CA_Final_Regia.Services.ActionFilters;
using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
namespace CA_Final_Regia.Services.Services.LocationServices
{
    public class LocationUpdateService(ILocationRepository locationRepository) : ILocationUpdateService
    {
        public async Task<ResponseDto<LocationDto>> UpdateLocationAsync(KeyValue locationUpdateKeyValue, Guid accountId)
        {
            try
            {
                var validation = locationUpdateKeyValue.ValidateKeyValue<LocationDto>();
                if (!validation.IsSuccess)
                {
                    return new ResponseDto<LocationDto>(false, validation.Message, ResponseDto<LocationDto>.Status.Bad_Request);
                }
                var location = await locationRepository.GetLocationAsync(accountId);
                if (location == null)
                {
                    return new ResponseDto<LocationDto>(false, "Location not found.", ResponseDto<LocationDto>.Status.Not_Found);
                }
                var response = await SwichLocationKeyValue(locationUpdateKeyValue, location);
                if (!response.IsSuccess)
                {
                    return new ResponseDto<LocationDto>(false, response.Message, ResponseDto<LocationDto>.Status.Bad_Request);
                }
                return new ResponseDto<LocationDto>(true, "Location updated", ResponseDto<LocationDto>.Status.Created);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        private async Task<ResponseDto<Location>> SwichLocationKeyValue(KeyValue personUpdateKeyValue, Location location)
        {
            try
            {
                string valueAsString = personUpdateKeyValue.Value.ToString();
                switch (personUpdateKeyValue.Key)
                {
                    case "City":
                        location.City = valueAsString;
                        break;
                    case "Street":
                        location.Street = valueAsString;
                        break;
                    case "HouseNr":
                        location.HouseNr = valueAsString;
                        break;
                    case "ApartmentNr":
                        location.ApartmentNr = valueAsString;
                        break;
                    default:
                        return new ResponseDto<Location>(false, "Bad Key", ResponseDto<Location>.Status.Bad_Request);
                }
                await locationRepository.UpdateLocationAsync(location);
                return new ResponseDto<Location>(true, "Location updated", ResponseDto<Location>.Status.Created);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
