using AutoFixture;
using AutoFixture.Kernel;
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
    public class PersonControllerTest
    {
        [Theory, AutoData]
        public async Task GetPersonInfoAsync_Return200O(PersonGetDto personGetDto)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            var accountId = Guid.NewGuid();
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<PersonGetDto>(true, personGetDto, ResponseDto<PersonGetDto>.Status.Ok);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(accountId);
            personGetInfoServiceMock.Setup(x => x.GetPersonInfoAsync(It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.GetPersonInfoAsync(It.IsAny<string>());
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response.Result);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task GetPersonInfoAsync_BadReqest_From_Servise_Return400BadReqest(PersonGetDto personGetDto)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            var accountId = Guid.NewGuid();
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<PersonGetDto>(false, "Bad request", ResponseDto<PersonGetDto>.Status.Bad_Request);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(accountId);
            personGetInfoServiceMock.Setup(x => x.GetPersonInfoAsync(It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.GetPersonInfoAsync(It.IsAny<string>());
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response.Result);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateFirstNamAsync_Returns201Created(KeyValue personUpdateKeyValue)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            var accountId = Guid.NewGuid();
            personUpdateKeyValue.Key = "FirstName";
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<PersonPostDto>(true, "First Name Updated", ResponseDto<PersonPostDto>.Status.Created);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(accountId);
            personUpdateServiceMock.Setup(x => x.UpdatePersonAsync(personUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.UpdateFirstNamAsync(It.IsAny<string>(), personUpdateKeyValue);
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateFirstNamAsync_Returns400BadRequest(KeyValue personUpdateKeyValue)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            var accountId = Guid.NewGuid();
            personUpdateKeyValue.Key = "FirstName";
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<PersonPostDto>(false, "Bad request", ResponseDto<PersonPostDto>.Status.Bad_Request);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(accountId);
            personUpdateServiceMock.Setup(x => x.UpdatePersonAsync(personUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.UpdateFirstNamAsync(It.IsAny<string>(), personUpdateKeyValue);
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateFirstNamAsync_When_Key_Is_Invalid_Returns400BadRequest(KeyValue personUpdateKeyValue)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            // Act
            var response = await sut.UpdateFirstNamAsync(It.IsAny<string>(), personUpdateKeyValue);
            // Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }

        [Theory, AutoData]
        public async Task UpdateLastNameAsync_Returns201Created(KeyValue personUpdateKeyValue)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            var accountId = Guid.NewGuid();
            personUpdateKeyValue.Key = "LastName";
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<PersonPostDto>(true, "Last Name Updated", ResponseDto<PersonPostDto>.Status.Created);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(accountId);
            personUpdateServiceMock.Setup(x => x.UpdatePersonAsync(personUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.UpdateLastNameAsync(It.IsAny<string>(), personUpdateKeyValue);
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateLastNameAsync_Returns400BadRequest(KeyValue personUpdateKeyValue)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            var accountId = Guid.NewGuid();
            personUpdateKeyValue.Key = "LastName";
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<PersonPostDto>(false, "Bad request", ResponseDto<PersonPostDto>.Status.Bad_Request);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(accountId);
            personUpdateServiceMock.Setup(x => x.UpdatePersonAsync(personUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.UpdateLastNameAsync(It.IsAny<string>(), personUpdateKeyValue);
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateLastNameAsync_When_Key_Is_Invalid_Returns400BadRequest(KeyValue personUpdateKeyValue)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            // Act
            var response = await sut.UpdateLastNameAsync(It.IsAny<string>(), personUpdateKeyValue);
            // Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdatePersonalIdAsync_Returns201Created(KeyValue personUpdateKeyValue)
        {             
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            var accountId = Guid.NewGuid();
            personUpdateKeyValue.Key = "PersonalId";
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<PersonPostDto>(true, "Personal Id Updated", ResponseDto<PersonPostDto>.Status.Created);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(accountId);
            personUpdateServiceMock.Setup(x => x.UpdatePersonAsync(personUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.UpdatePersonalIdAsync(It.IsAny<string>(), personUpdateKeyValue);
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdatePersonalIdAsync_Returns400BadRequest(KeyValue personUpdateKeyValue)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            var accountId = Guid.NewGuid();
            personUpdateKeyValue.Key = "PersonalId";
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<PersonPostDto>(false, "Bad request", ResponseDto<PersonPostDto>.Status.Bad_Request);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(accountId);
            personUpdateServiceMock.Setup(x => x.UpdatePersonAsync(personUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.UpdatePersonalIdAsync(It.IsAny<string>(), personUpdateKeyValue);
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdatePersonalIdAsync_When_Key_Is_Invalid_Returns400BadRequest(KeyValue personUpdateKeyValue)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            // Act
            var response = await sut.UpdatePersonalIdAsync(It.IsAny<string>(), personUpdateKeyValue);
            // Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdatePhoneNumberAsync_Returns201Created(KeyValue personUpdateKeyValue)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            var accountId = Guid.NewGuid();
            personUpdateKeyValue.Key = "PhoneNumber";
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<PersonPostDto>(true, "Phone Number Updated", ResponseDto<PersonPostDto>.Status.Created);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(accountId);
            personUpdateServiceMock.Setup(x => x.UpdatePersonAsync(personUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.UpdatePhoneNumberAsync(It.IsAny<string>(), personUpdateKeyValue);
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdatePhoneNumberAsync_Returns400BadRequest(KeyValue personUpdateKeyValue)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            var accountId = Guid.NewGuid();
            personUpdateKeyValue.Key = "PhoneNumber";
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<PersonPostDto>(false, "Bad request", ResponseDto<PersonPostDto>.Status.Bad_Request);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(accountId);
            personUpdateServiceMock.Setup(x => x.UpdatePersonAsync(personUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.UpdatePhoneNumberAsync(It.IsAny<string>(), personUpdateKeyValue);
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdatePhoneNumberAsync_When_Key_Is_Invalid_Returns400BadRequest(KeyValue personUpdateKeyValue)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            // Act
            var response = await sut.UpdatePhoneNumberAsync(It.IsAny<string>(), personUpdateKeyValue);
            // Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateMailAsync_Returns201Created(KeyValue personUpdateKeyValue)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            var accountId = Guid.NewGuid();
            personUpdateKeyValue.Key = "Mail";
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<PersonPostDto>(true, "Mail Updated", ResponseDto<PersonPostDto>.Status.Created);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(accountId);
            personUpdateServiceMock.Setup(x => x.UpdatePersonAsync(personUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.UpdateMaildAsync(It.IsAny<string>(), personUpdateKeyValue);
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateMailAsync_Returns400BadRequest(KeyValue personUpdateKeyValue)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            var accountId = Guid.NewGuid();
            personUpdateKeyValue.Key = "Mail";
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            var expectedResponse = new ResponseDto<PersonPostDto>(false, "Bad request", ResponseDto<PersonPostDto>.Status.Bad_Request);
            jwtExtraxtSereviceMock.Setup(x => x.GetAccountIdFromJwtToken(It.IsAny<string>())).Returns(accountId);
            personUpdateServiceMock.Setup(x => x.UpdatePersonAsync(personUpdateKeyValue, It.IsAny<Guid>())).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.UpdateMaildAsync(It.IsAny<string>(), personUpdateKeyValue);
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task UpdateMailAsync_When_Key_Is_Invalid_Returns400BadRequest(KeyValue personUpdateKeyValue)
        {
            // Arrange
            var personAddInfoServiseMock = new Mock<IPersonAddInfoService>();
            var logerMock = new Mock<ILogger<PersonController>>();
            var jwtExtraxtSereviceMock = new Mock<IJwtExtractService>();
            var personGetInfoServiceMock = new Mock<IPersonGetInfoService>();
            var personUpdateServiceMock = new Mock<IPersonUpdateService>();
            //sut - Subject Under Test
            var sut = new PersonController(personAddInfoServiseMock.Object, logerMock.Object, jwtExtraxtSereviceMock.Object, personGetInfoServiceMock.Object, personUpdateServiceMock.Object);
            // Act
            var response = await sut.UpdateMaildAsync(It.IsAny<string>(), personUpdateKeyValue);
            // Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}
