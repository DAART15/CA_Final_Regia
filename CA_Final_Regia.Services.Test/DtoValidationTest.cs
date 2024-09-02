using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Services.ValidationService;
using CA_Final_Regia.Services.Test.LocationDtoData;

namespace CA_Final_Regia.Services.Test
{
    public class DtoValidationTest
    {
        [Theory, LocationDtoData]
        public void DtoKeyValueValidation_WhenDtoIsValid_ReturnsOk(LocationDto locationDto)
        {
            // Arrange
            var sut = new DtoValidation<LocationDto>();
            // Act
            var result = sut.DtoKeyValueValidation(locationDto);
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Dto Object info is valid", result.Message);
            Assert.Equal(ResponseDto<LocationDto>.Status.Ok, result.StatusCode);
        }
        [Fact]
        public void DtoKeyValueValidation_When_LocationDto_ApartmentNr_IsEmty_ReturnsBadRequest_And_Message()
        {
            // Arrange
            var locationDto = new LocationDto
            {
                City = "Jonava",
                Street = "Basanavičiaus",
                HouseNr = "35A",
                ApartmentNr = ""
            };
            var expectedResponse = new ResponseDto<LocationDto>(false, "Apartment number is required", ResponseDto<LocationDto>.Status.Bad_Request);
            var sut = new DtoValidation<LocationDto>();
            // Act
            var result = sut.DtoKeyValueValidation(locationDto);
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedResponse.Message, result.Message);
            Assert.Equal(expectedResponse.StatusCode, result.StatusCode);
        }
        [Fact]
        public void DtoKeyValueValidation_When_LocationDto_ApartmentNr_IsToLong_ReturnsBadRequest_And_Message()
        {
            // Arrange
            var locationDto = new LocationDto
            {
                City = "Jonava",
                Street = "Basanavičiaus",
                HouseNr = "35A",
                ApartmentNr = "1234A"
            };
            var expectedResponse = new ResponseDto<LocationDto>(false, "Apartment number is too long", ResponseDto<LocationDto>.Status.Bad_Request);
            var sut = new DtoValidation<LocationDto>();
            // Act
            var result = sut.DtoKeyValueValidation(locationDto);
            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedResponse.Message, result.Message);
            Assert.Equal(expectedResponse.StatusCode, result.StatusCode);
        }
    }
}
