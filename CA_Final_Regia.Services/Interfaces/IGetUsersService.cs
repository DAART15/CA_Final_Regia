using CA_Final_Regia.Services.DTOs;
namespace CA_Final_Regia.Services.Interfaces
{
    public interface IGetUsersService
    {
        Task<ResponseDto<AccountDto>> GetUsersAsync();
    }
}
