using CA_Final_Regia.Services.DTOs;
namespace CA_Final_Regia.Services.Interfaces
{
    public interface IDeleteUserService
    {
        Task<ResponseDto<AccountDto>> DeleteUserAsync(Guid accountId);
    }
}
