
using AutoFixture.Xunit2;
using CA_Final_Regia.Domain.Entities;
using CA_Final_Regia.Domain.Interfaces.Repository;
using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Services.UserServices;
using Moq;

namespace CA_Final_Regia.Services.Test
{
    public class UserLogInServiceTest
    {
        [Theory, AutoData]
        public async Task LogInAsync_WhenUserExists_Returns404NotFound(User user)
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();

            //sut - Subject Under Test
            var sut = new UserLogInService(accountRepositoryMock.Object);
            var expectedResponse = new ResponseDto<Account>(false, "User not found", ResponseDto<Account>.Status.Not_Found);
            accountRepositoryMock.Setup(repo => repo.GetAccountByUserNameAsync(user.UserName)).ReturnsAsync((Account)null);

            // Act
            var result = await sut.LogInAsync(user);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedResponse.StatusCode, result.StatusCode);
        }
    }
}
