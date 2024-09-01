using AutoFixture.Xunit2;
using CA_Final_Regia.Domain.Entities;
using CA_Final_Regia.Domain.Interfaces.Repository;
using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Services.UserServices;
using Moq;

namespace CA_Final_Regia.Services.Test
{
    public class UserRegisterServiceTest
    {
        [Theory, AutoData]
        public async Task RegisterAsync_WhenUserExists_ReturnsBadRequest(User user)
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            
            //sut - Subject Under Test
            var sut = new UserRegisterService(accountRepositoryMock.Object);
            var expectedResponse = new ResponseDto<User>(false, $"User already exists.", ResponseDto<User>.Status.Bad_Request);
            accountRepositoryMock.Setup(repo => repo.GetAccountByUserNameAsync(user.UserName)).ReturnsAsync(new Account());
            // Act
            var result = await sut.RegisterAsync(user);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedResponse.StatusCode, result.StatusCode);
        }
    }
}