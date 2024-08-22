using CA_Final_Regia.DTOs;

namespace CA_Final_Regia.Interfaces
{
    public interface IGetUsersService
    {
        Task<ResponseDto<AccountDto>> GetUsersAsync();
    }
}
