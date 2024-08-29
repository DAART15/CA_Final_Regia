using AutoFixture.Xunit2;
using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
using CA_Final_Regia.Web.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CA_Final_Regia.Web.API.Test
{
    public class LocationConrollerTest
    {
        [Theory, AutoData]
        public async Task AddLocationAsync_WhenTokenIsInvalid_Return401Unauthorized(LocationDto locationDto)
        {
            // Arrange
            var locationAddServiceMock = new Mock<ILocationAddService>();
            var locationGetServiceMock = new Mock<ILocationGetService>();
            var locationUpdateServiceMock = new Mock<ILocationUpdateService>();
            var loggerMock = new Mock<ILogger<LocationController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var invalidGuid = Guid.Empty;
            //sut - Subject Under Test
            var sut = new LocationController(loggerMock.Object, jwtExtraxtSereviceMock.Object, locationAddServiceMock.Object, locationGetServiceMock.Object, locationUpdateServiceMock.Object);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(invalidGuid);
            // Act
            var response = await sut.AddLocationAsync(It.IsAny<string>(), locationDto);
            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(response);
            Assert.Equal(StatusCodes.Status401Unauthorized, unauthorizedResult.StatusCode);
            Assert.Equal("Token is invalid", unauthorizedResult.Value);
        }
        [Theory, AutoData]
        public async Task AddLocationAsync_Return201Created(LocationDto locationDto)
        {
            //Arrange
            var locationAddServiceMock = new Mock<ILocationAddService>();
            var locationGetServiceMock = new Mock<ILocationGetService>();
            var locationUpdateServiceMock = new Mock<ILocationUpdateService>();
            var loggerMock = new Mock<ILogger<LocationController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var anyGuid = Guid.NewGuid();
            //sut - Subject Under Test
            var sut = new LocationController(loggerMock.Object, jwtExtraxtSereviceMock.Object, locationAddServiceMock.Object, locationGetServiceMock.Object, locationUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<LocationDto>(true, "Location added successfully", ResponseDto<LocationDto>.Status.Created);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(anyGuid);
            locationAddServiceMock.Setup(x => x.AddLocationAsync(locationDto, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.AddLocationAsync(It.IsAny<string>(), locationDto);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task AddLocationAsync_Return500InternalServerError(LocationDto locationDto)
        {
            //Arrange
            var locationAddServiceMock = new Mock<ILocationAddService>();
            var locationGetServiceMock = new Mock<ILocationGetService>();
            var locationUpdateServiceMock = new Mock<ILocationUpdateService>();
            var loggerMock = new Mock<ILogger<LocationController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var anyGuid = Guid.NewGuid();
            //sut - Subject Under Test
            var sut = new LocationController(loggerMock.Object, jwtExtraxtSereviceMock.Object, locationAddServiceMock.Object, locationGetServiceMock.Object, locationUpdateServiceMock.Object);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(anyGuid);
            locationAddServiceMock.Setup(x => x.AddLocationAsync(locationDto, It.IsAny<Guid>())).ThrowsAsync(new ArgumentException());
            //Act
            var response = await sut.AddLocationAsync(It.IsAny<string>(), locationDto);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            Assert.Equal("An error occurred while adding a new location.", objectResult.Value);
        }
        [Theory, AutoData]
        public async Task GetLocationAsync_BadReqest_From_Servise_Return400BadReqest(LocationDto locationDto)
        {
            //Arrange
            var locationAddServiceMock = new Mock<ILocationAddService>();
            var locationGetServiceMock = new Mock<ILocationGetService>();
            var locationUpdateServiceMock = new Mock<ILocationUpdateService>();
            var loggerMock = new Mock<ILogger<LocationController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var anyGuid = Guid.NewGuid();
            //sut - Subject Under Test
            var sut = new LocationController(loggerMock.Object, jwtExtraxtSereviceMock.Object, locationAddServiceMock.Object, locationGetServiceMock.Object, locationUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<LocationDto>(false, "Bad Request", ResponseDto<LocationDto>.Status.Bad_Request);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(anyGuid);
            locationGetServiceMock.Setup(x => x.GetLocationAsync(It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.GetLocationAsync(It.IsAny<string>());
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response.Result);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task GetLocationAsync_Return200OK(LocationDto locationDto)
        {
            //Arrange
            var locationAddServiceMock = new Mock<ILocationAddService>();
            var locationGetServiceMock = new Mock<ILocationGetService>();
            var locationUpdateServiceMock = new Mock<ILocationUpdateService>();
            var loggerMock = new Mock<ILogger<LocationController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var anyGuid = Guid.NewGuid();
            //sut - Subject Under Test
            var sut = new LocationController(loggerMock.Object, jwtExtraxtSereviceMock.Object, locationAddServiceMock.Object, locationGetServiceMock.Object, locationUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<LocationDto>(true, locationDto, ResponseDto<LocationDto>.Status.Ok);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(anyGuid);
            locationGetServiceMock.Setup(x => x.GetLocationAsync(It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.GetLocationAsync(It.IsAny<string>());
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response.Result);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateCityAsync_Returns201Created(KeyValue locationUpdateKeyValue)
        {
            //Arrange
            var locationAddServiceMock = new Mock<ILocationAddService>();
            var locationGetServiceMock = new Mock<ILocationGetService>();
            var locationUpdateServiceMock = new Mock<ILocationUpdateService>();
            var loggerMock = new Mock<ILogger<LocationController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var anyGuid = Guid.NewGuid();
            //sut - Subject Under Test
            var sut = new LocationController(loggerMock.Object, jwtExtraxtSereviceMock.Object, locationAddServiceMock.Object, locationGetServiceMock.Object, locationUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<LocationDto>(true, "City updated", ResponseDto<LocationDto>.Status.Created);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(anyGuid);
            locationUpdateServiceMock.Setup(x => x.UpdateLocationAsync(locationUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.UpdateCityAsync(It.IsAny<string>(), locationUpdateKeyValue);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateCityAsync_Returns400BadRequest(KeyValue locationUpdateKeyValue)
        {
            //Arrange
            var locationAddServiceMock = new Mock<ILocationAddService>();
            var locationGetServiceMock = new Mock<ILocationGetService>();
            var locationUpdateServiceMock = new Mock<ILocationUpdateService>();
            var loggerMock = new Mock<ILogger<LocationController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var anyGuid = Guid.NewGuid();
            //sut - Subject Under Test
            var sut = new LocationController(loggerMock.Object, jwtExtraxtSereviceMock.Object, locationAddServiceMock.Object, locationGetServiceMock.Object, locationUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<LocationDto>(false, "Bad Request", ResponseDto<LocationDto>.Status.Bad_Request);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(anyGuid);
            locationUpdateServiceMock.Setup(x => x.UpdateLocationAsync(locationUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.UpdateCityAsync(It.IsAny<string>(), locationUpdateKeyValue);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateStreetAsync_Returns201Created(KeyValue locationUpdateKeyValue)
        {
            //Arrange
            var locationAddServiceMock = new Mock<ILocationAddService>();
            var locationGetServiceMock = new Mock<ILocationGetService>();
            var locationUpdateServiceMock = new Mock<ILocationUpdateService>();
            var loggerMock = new Mock<ILogger<LocationController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var anyGuid = Guid.NewGuid();
            //sut - Subject Under Test
            var sut = new LocationController(loggerMock.Object, jwtExtraxtSereviceMock.Object, locationAddServiceMock.Object, locationGetServiceMock.Object, locationUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<LocationDto>(true, "Street updated", ResponseDto<LocationDto>.Status.Created);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(anyGuid);
            locationUpdateServiceMock.Setup(x => x.UpdateLocationAsync(locationUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.UpdateStreetAsync(It.IsAny<string>(), locationUpdateKeyValue);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateStreetAsync_Returns400BadRequest(KeyValue locationUpdateKeyValue)
        {
            //Arrange
            var locationAddServiceMock = new Mock<ILocationAddService>();
            var locationGetServiceMock = new Mock<ILocationGetService>();
            var locationUpdateServiceMock = new Mock<ILocationUpdateService>();
            var loggerMock = new Mock<ILogger<LocationController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var anyGuid = Guid.NewGuid();
            //sut - Subject Under Test
            var sut = new LocationController(loggerMock.Object, jwtExtraxtSereviceMock.Object, locationAddServiceMock.Object, locationGetServiceMock.Object, locationUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<LocationDto>(false, "Bad Request", ResponseDto<LocationDto>.Status.Bad_Request);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(anyGuid);
            locationUpdateServiceMock.Setup(x => x.UpdateLocationAsync(locationUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.UpdateStreetAsync(It.IsAny<string>(), locationUpdateKeyValue);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateHouseNrAsync_Returns201Created(KeyValue locationUpdateKeyValue)
        {
            //Arrange
            var locationAddServiceMock = new Mock<ILocationAddService>();
            var locationGetServiceMock = new Mock<ILocationGetService>();
            var locationUpdateServiceMock = new Mock<ILocationUpdateService>();
            var loggerMock = new Mock<ILogger<LocationController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var anyGuid = Guid.NewGuid();
            //sut - Subject Under Test
            var sut = new LocationController(loggerMock.Object, jwtExtraxtSereviceMock.Object, locationAddServiceMock.Object, locationGetServiceMock.Object, locationUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<LocationDto>(true, "HouseNr updated", ResponseDto<LocationDto>.Status.Created);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(anyGuid);
            locationUpdateServiceMock.Setup(x => x.UpdateLocationAsync(locationUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.UpdateHouseNrAsync(It.IsAny<string>(), locationUpdateKeyValue);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateHouseNrAsync_Returns400BadRequest(KeyValue locationUpdateKeyValue)
        {
            //Arrange
            var locationAddServiceMock = new Mock<ILocationAddService>();
            var locationGetServiceMock = new Mock<ILocationGetService>();
            var locationUpdateServiceMock = new Mock<ILocationUpdateService>();
            var loggerMock = new Mock<ILogger<LocationController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var anyGuid = Guid.NewGuid();
            //sut - Subject Under Test
            var sut = new LocationController(loggerMock.Object, jwtExtraxtSereviceMock.Object, locationAddServiceMock.Object, locationGetServiceMock.Object, locationUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<LocationDto>(false, "Bad Request", ResponseDto<LocationDto>.Status.Bad_Request);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(anyGuid);
            locationUpdateServiceMock.Setup(x => x.UpdateLocationAsync(locationUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.UpdateHouseNrAsync(It.IsAny<string>(), locationUpdateKeyValue);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateApartmentNrAsync_Returns201Created(KeyValue locationUpdateKeyValue)
        {
            //Arrange
            var locationAddServiceMock = new Mock<ILocationAddService>();
            var locationGetServiceMock = new Mock<ILocationGetService>();
            var locationUpdateServiceMock = new Mock<ILocationUpdateService>();
            var loggerMock = new Mock<ILogger<LocationController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var anyGuid = Guid.NewGuid();
            //sut - Subject Under Test
            var sut = new LocationController(loggerMock.Object, jwtExtraxtSereviceMock.Object, locationAddServiceMock.Object, locationGetServiceMock.Object, locationUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<LocationDto>(true, "ApartmentNr updated", ResponseDto<LocationDto>.Status.Created);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(anyGuid);
            locationUpdateServiceMock.Setup(x => x.UpdateLocationAsync(locationUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.UpdateApartmentNrAsync(It.IsAny<string>(), locationUpdateKeyValue);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateApartmentNrAsync_Returns400BadRequest(KeyValue locationUpdateKeyValue)
        {
            //Arrange
            var locationAddServiceMock = new Mock<ILocationAddService>();
            var locationGetServiceMock = new Mock<ILocationGetService>();
            var locationUpdateServiceMock = new Mock<ILocationUpdateService>();
            var loggerMock = new Mock<ILogger<LocationController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var anyGuid = Guid.NewGuid();
            //sut - Subject Under Test
            var sut = new LocationController(loggerMock.Object, jwtExtraxtSereviceMock.Object, locationAddServiceMock.Object, locationGetServiceMock.Object, locationUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<LocationDto>(false, "Bad Request", ResponseDto<LocationDto>.Status.Bad_Request);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(anyGuid);
            locationUpdateServiceMock.Setup(x => x.UpdateLocationAsync(locationUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.UpdateApartmentNrAsync(It.IsAny<string>(), locationUpdateKeyValue);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
    }
}
