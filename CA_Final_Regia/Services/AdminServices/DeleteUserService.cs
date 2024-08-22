using CA_Final_Regia.DTOs;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;

namespace CA_Final_Regia.Services.AdminServices
{
    public class DeleteUserService(IAccountRepository accountRepository) : IDeleteUserService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        public async Task<ResponseDto<AccountDto>> DeleteUserAsync(Guid accountId)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                return new ResponseDto<AccountDto>(false, "Account not found", ResponseDto<AccountDto>.Status.Not_Found);
            }
            await _accountRepository.DeleteAccountAsync(account);
            return new ResponseDto<AccountDto>(true, "Account deleted", ResponseDto<AccountDto>.Status.Ok);
        }
    }
}
