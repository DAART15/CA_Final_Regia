using AutoFixture.Xunit2;
using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
using CA_Final_Regia.Web.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CA_Final_Regia.Web.API.Test
{
    public class UserControllerTest
    {
        [Theory, AutoData]
        public async Task Register_Return201Created(User user)
        {
            //Arrange
            var userLogInService = new Mock<IUserLogInService>();
            var userRegisterService = new Mock<IUserRegisterService>();
            var logger = new Mock<ILogger<UserController>>();
            var jwtService = new Mock<IJwtTokenService>();
            //sut - Subject Under Test
            var sut = new UserController(userLogInService.Object, userRegisterService.Object, logger.Object, jwtService.Object);
            var expextedResponse = new ResponseDto<User>(true, "Account for User created successfully", ResponseDto<User>.Status.Created);
            userRegisterService.Setup(x => x.RegisterAsync(user)).ReturnsAsync(expextedResponse);
            //Act
            var response = await sut.RegisterAsync(user);
            //Assert
            Assert.Equal((int)expextedResponse.StatusCode, (response as ObjectResult).StatusCode);
        }
    }
}