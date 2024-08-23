using CA_Final_Regia.DTOs;
using System.Text.RegularExpressions;

namespace CA_Final_Regia.Properties.ActionFilters
{
    public static partial class LocationDtoValidationExtension
    {
        public static ResponseDto<LocationDto> ValidateLocationDto(this LocationDto locationDto)
        {
            if (locationDto == null)
            {
                return new ResponseDto<LocationDto>(false, "Location object is null", ResponseDto<LocationDto>.Status.Bad_Request);
            }
            if (string.IsNullOrWhiteSpace(locationDto.City))
            {
                return new ResponseDto<LocationDto>(false, "City is required", ResponseDto<LocationDto>.Status.Bad_Request);
            }
            if (string.IsNullOrWhiteSpace(locationDto.Street))
            {
                return new ResponseDto<LocationDto>(false, "Street is required", ResponseDto<LocationDto>.Status.Bad_Request);
            }
            if (!HouseNrRegex().IsMatch(locationDto.HouseNr))
            {
                return new ResponseDto<LocationDto>(false, "House number is required", ResponseDto<LocationDto>.Status.Bad_Request);
            }
            if (!ApartmentNrRegex().IsMatch(locationDto.ApartmentNr))
            {
                return new ResponseDto<LocationDto>(false, "Apartment number is required", ResponseDto<LocationDto>.Status.Bad_Request);
            }
            return new ResponseDto<LocationDto>(true, "Location is valid", ResponseDto<LocationDto>.Status.Ok);
        }

        [GeneratedRegex(@"^[a-zA-Z0-9]{1,5}$")]
        private static partial Regex HouseNrRegex();
        [GeneratedRegex(@"^[a-zA-Z0-9]{1,4}$")]
        private static partial Regex ApartmentNrRegex();
    }
}
