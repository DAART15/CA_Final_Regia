using CA_Final_Regia.Domain.Interfaces.Repository;
using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
namespace CA_Final_Regia.Services.Services.AdminServices
{
    public class DeleteUserService(IAccountRepository accountRepository) : IDeleteUserService
    {
        public async Task<ResponseDto<AccountDto>> DeleteUserAsync(Guid accountId)
        {
            try
            {
                var account = await accountRepository.GetAccountByIdAsync(accountId);
                if (account == null)
                {
                    return new ResponseDto<AccountDto>(false, "Account not found", ResponseDto<AccountDto>.Status.Not_Found);
                }
                await accountRepository.DeleteAccountAsync(account);
                return new ResponseDto<AccountDto>(true, "Account deleted", ResponseDto<AccountDto>.Status.Ok);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
