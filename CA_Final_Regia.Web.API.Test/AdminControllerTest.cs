using AutoFixture.Xunit2;
using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
using CA_Final_Regia.Web.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CA_Final_Regia.Web.API.Test
{
    public class AdminControllerTest
    {
        [Theory, AutoData]
        public async Task GetUsersAsync_Return200OK(IList<AccountDto> accountDtos)
        {
            // Arrange
            var getUsersServiceMock = new Mock<IGetUsersService>();
            var loggerMock = new Mock<ILogger<AdminController>>();
            var deleteUserServiceMock = new Mock<IDeleteUserService>();
            // sut - Subject Under Test
            var sut = new AdminController(getUsersServiceMock.Object, loggerMock.Object, deleteUserServiceMock.Object);
            var expectedResponse = new ResponseDto<AccountDto>(true, accountDtos, ResponseDto<AccountDto>.Status.Ok);
            getUsersServiceMock.Setup(x => x.GetUsersAsync()).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.GetUsersAsync();
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response.Result);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task GetUsersAsync_ReturnList(IList<AccountDto> accountDtos)
        {
            // Arrange
            var getUsersServiceMock = new Mock<IGetUsersService>();
            var loggerMock = new Mock<ILogger<AdminController>>();
            var deleteUserServiceMock = new Mock<IDeleteUserService>();
            // sut - Subject Under Test
            var sut = new AdminController(getUsersServiceMock.Object, loggerMock.Object, deleteUserServiceMock.Object);
            var expectedResponse = new ResponseDto<AccountDto>(true, accountDtos, ResponseDto<AccountDto>.Status.Ok);
            getUsersServiceMock.Setup(x => x.GetUsersAsync()).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.GetUsersAsync();
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response.Result);
            Assert.Equal(expectedResponse.List, objectResult.Value);
        }
        [Fact]
        public async Task GetUsersAsync_Not_Found_In_Servise_Return_4004NotFound()
        {
            // Arrange
            var getUsersServiceMock = new Mock<IGetUsersService>();
            var loggerMock = new Mock<ILogger<AdminController>>();
            var deleteUserServiceMock = new Mock<IDeleteUserService>();
            // sut - Subject Under Test
            var sut = new AdminController(getUsersServiceMock.Object, loggerMock.Object, deleteUserServiceMock.Object);
            var expectedResponse = new ResponseDto<AccountDto>(false, "Not Found", ResponseDto<AccountDto>.Status.Not_Found);
            getUsersServiceMock.Setup(x => x.GetUsersAsync()).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.GetUsersAsync();
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response.Result);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Fact]
        public async Task GetUsersAsync_Not_Found_In_Servise_Return_Mesage()
        {
            // Arrange
            var getUsersServiceMock = new Mock<IGetUsersService>();
            var loggerMock = new Mock<ILogger<AdminController>>();
            var deleteUserServiceMock = new Mock<IDeleteUserService>();
            // sut - Subject Under Test
            var sut = new AdminController(getUsersServiceMock.Object, loggerMock.Object, deleteUserServiceMock.Object);
            var expectedResponse = new ResponseDto<AccountDto>(false, "Not Found", ResponseDto<AccountDto>.Status.Not_Found);
            getUsersServiceMock.Setup(x => x.GetUsersAsync()).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.GetUsersAsync();
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response.Result);
            Assert.Equal((string)expectedResponse.Message, objectResult.Value);
        }
        [Theory, AutoData]
        public async Task DeleteUserByIdAsync_Return200OK(Guid accountId)
        {
            // Arrange
            var getUsersServiceMock = new Mock<IGetUsersService>();
            var loggerMock = new Mock<ILogger<AdminController>>();
            var deleteUserServiceMock = new Mock<IDeleteUserService>();
            // sut - Subject Under Test
            var sut = new AdminController(getUsersServiceMock.Object, loggerMock.Object, deleteUserServiceMock.Object);
            var expectedResponse = new ResponseDto<AccountDto>(true, "User deleted", ResponseDto<AccountDto>.Status.Ok);
            deleteUserServiceMock.Setup(x => x.DeleteUserAsync(accountId)).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.DeleteUserByIdAsync(accountId);
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task DeleteUserByIdAsync_Not_Found_In_Servise_Return_4004NotFound(Guid accountId)
        {
            // Arrange
            var getUsersServiceMock = new Mock<IGetUsersService>();
            var loggerMock = new Mock<ILogger<AdminController>>();
            var deleteUserServiceMock = new Mock<IDeleteUserService>();
            // sut - Subject Under Test
            var sut = new AdminController(getUsersServiceMock.Object, loggerMock.Object, deleteUserServiceMock.Object);
            var expectedResponse = new ResponseDto<AccountDto>(false, "User not found", ResponseDto<AccountDto>.Status.Not_Found);
            deleteUserServiceMock.Setup(x => x.DeleteUserAsync(accountId)).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.DeleteUserByIdAsync(accountId);
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((int)expectedResponse.StatusCode, objectResult.StatusCode);
        }
        [Theory, AutoData]
        public async Task DeleteUserByIdAsync_Not_Found_In_Servise_Return_Mesage(Guid accountId)
        {
            // Arrange
            var getUsersServiceMock = new Mock<IGetUsersService>();
            var loggerMock = new Mock<ILogger<AdminController>>();
            var deleteUserServiceMock = new Mock<IDeleteUserService>();
            // sut - Subject Under Test
            var sut = new AdminController(getUsersServiceMock.Object, loggerMock.Object, deleteUserServiceMock.Object);
            var expectedResponse = new ResponseDto<AccountDto>(false, "User not found", ResponseDto<AccountDto>.Status.Not_Found);
            deleteUserServiceMock.Setup(x => x.DeleteUserAsync(accountId)).ReturnsAsync(expectedResponse);
            // Act
            var response = await sut.DeleteUserByIdAsync(accountId);
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal((string)expectedResponse.Message, objectResult.Value);
        }
    }
}
