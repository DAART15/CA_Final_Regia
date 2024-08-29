using AutoFixture.Xunit2;
using CA_Final_Regia.Domain.Entities;
using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
using CA_Final_Regia.Services.Services.JwtServices;
using CA_Final_Regia.Web.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CA_Final_Regia.Web.API.Test
{
    public class UserControllerTest
    {
        [Theory, AutoData]
        public async Task RegisterAsync_Return201Created(User user)
        {
            //Arrange
            var userLogInServiceMock = new Mock<IUserLogInService>();
            var userRegisterServiceMock = new Mock<IUserRegisterService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var jwtServiceMock = new Mock<IJwtTokenService>();
            //sut - Subject Under Test
            var sut = new UserController(userLogInServiceMock.Object, userRegisterServiceMock.Object, loggerMock.Object, jwtServiceMock.Object);
            var expectedResponse = new ResponseDto<User>(true, "Account for User created successfully", ResponseDto<User>.Status.Created);
            userRegisterServiceMock.Setup(x => x.RegisterAsync(user)).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.RegisterAsync(user);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task RegisterAsync_Return_400BadRequest(User user)
        {
            //Arrange
            var userLogInServiceMock = new Mock<IUserLogInService>();
            var userRegisterServiceMock = new Mock<IUserRegisterService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var jwtServiceMock = new Mock<IJwtTokenService>();
            //sut - Subject Under Test
            var sut = new UserController(userLogInServiceMock.Object, userRegisterServiceMock.Object, loggerMock.Object, jwtServiceMock.Object);
            var expectedResponse = new ResponseDto<User>(false, "Account for User not created", ResponseDto<User>.Status.Bad_Request);
            userRegisterServiceMock.Setup(x => x.RegisterAsync(user)).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.RegisterAsync(user);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task RegisterAsync_Assume_Bad_UserName_Return_UserName_Mesage(User user)
        {
            //Arrange
            var userLogInServiceMock = new Mock<IUserLogInService>();
            var userRegisterServiceMock = new Mock<IUserRegisterService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var jwtServiceMock = new Mock<IJwtTokenService>();
            //sut - Subject Under Test
            var sut = new UserController(userLogInServiceMock.Object, userRegisterServiceMock.Object, loggerMock.Object, jwtServiceMock.Object);
            var expectedResponse = new ResponseDto<User>(false, "Nickname is not valid.", ResponseDto<User>.Status.Bad_Request);
            userRegisterServiceMock.Setup(x => x.RegisterAsync(user)).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.RegisterAsync(user);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((string)expectedResponse.Message, objectResult.Value);
        }
        [Theory, AutoData]
        public async Task RegisterAsync_Assume_Bad_Password_Return_Password_Mesage(User user)
        {
            //Arrange
            var userLogInServiceMock = new Mock<IUserLogInService>();
            var userRegisterServiceMock = new Mock<IUserRegisterService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var jwtServiceMock = new Mock<IJwtTokenService>();
            //sut - Subject Under Test
            var sut = new UserController(userLogInServiceMock.Object, userRegisterServiceMock.Object, loggerMock.Object, jwtServiceMock.Object);
            var expectedResponse = new ResponseDto<User>(false, "Password is not valid.", ResponseDto<User>.Status.Bad_Request);
            userRegisterServiceMock.Setup(x => x.RegisterAsync(user)).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.RegisterAsync(user);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((string)expectedResponse.Message, objectResult.Value);
        }
        [Theory, AutoData]
        public async Task LogInAsync_Assume_Bad_Password_Return_404NotFound(User user)
        {
            //Arrange
            var userLogInServiceMock = new Mock<IUserLogInService>();
            var userRegisterServiceMock = new Mock<IUserRegisterService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var jwtServiceMock = new Mock<IJwtTokenService>();
            //sut - Subject Under Test
            var sut = new UserController(userLogInServiceMock.Object, userRegisterServiceMock.Object, loggerMock.Object, jwtServiceMock.Object);
            var expectedResponse = new ResponseDto<Account>(false, "User not found", ResponseDto<Account>.Status.Not_Found);
            userLogInServiceMock.Setup(x => x.LogInAsync(user)).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.LogInAsync(user);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response.Result);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task LogInAsync_Assume_Bad_Password_Return_Password_Mesage(User user )
        {
            //Arrange
            var userLogInServiceMock = new Mock<IUserLogInService>();
            var userRegisterServiceMock = new Mock<IUserRegisterService>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var jwtServiceMock = new Mock<IJwtTokenService>();
            //sut - Subject Under Test
            var sut = new UserController(userLogInServiceMock.Object, userRegisterServiceMock.Object, loggerMock.Object, jwtServiceMock.Object);
            var expectedResponse = new ResponseDto<Account>(false, "Bad password", ResponseDto<Account>.Status.Not_Found);
            userLogInServiceMock.Setup(x => x.LogInAsync(user)).ReturnsAsync(expectedResponse);
            //Act
            var response = await sut.LogInAsync(user);
            //Assert
            var objectResult = Assert.IsType<ObjectResult>(response.Result);
            Assert.Equal((string)expectedResponse.Message, objectResult.Value);
        }
    }
}