using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.DTOs;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;
using CA_Final_Regia.Properties.ActionFilters;

namespace CA_Final_Regia.Services.LocationServices
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
                    return new ResponseDto<LocationDto>(false, "Person not found.", ResponseDto<LocationDto>.Status.Not_Found);
                }
                var response = await SwichPersonKeyValue(locationUpdateKeyValue, location);
                if (!response.IsSuccess)
                {
                    return new ResponseDto<LocationDto>(false, response.Message, ResponseDto<LocationDto>.Status.Bad_Request);
                }
                return new ResponseDto<LocationDto>(true, "Person updated", ResponseDto<LocationDto>.Status.Created);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        private async Task<ResponseDto<Location>> SwichPersonKeyValue(KeyValue personUpdateKeyValue, Location location)
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
                var response = await locationRepository.UpdateLocationAsync(location);
                if (response == null)
                {
                    return new ResponseDto<Location>(false, "Person not updated", ResponseDto<Location>.Status.Internal_Server_Error);
                }
                return new ResponseDto<Location>(true, "Location updated", ResponseDto<Location>.Status.Created);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
