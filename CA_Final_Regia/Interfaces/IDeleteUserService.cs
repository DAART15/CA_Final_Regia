using CA_Final_Regia.DTOs;

namespace CA_Final_Regia.Interfaces
{
    public interface IDeleteUserService
    {
        Task<ResponseDto<AccountDto>> DeleteUserAsync(Guid accountId);
    }
}
