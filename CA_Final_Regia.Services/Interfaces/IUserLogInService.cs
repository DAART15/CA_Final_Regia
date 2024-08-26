using CA_Final_Regia.Domain.Entities;
using CA_Final_Regia.Services.DTOs;
namespace CA_Final_Regia.Services.Interfaces
{
    public interface IUserLogInService
    {
        Task<ResponseDto<Account>> LogInAsync(User user);
    }
}
