using CA_Final_Regia.Services.DTOs;
namespace CA_Final_Regia.Services.Interfaces
{
    public interface IUserRegisterService
    {
        Task<ResponseDto<User>> RegisterAsync(User user);
    }
}
