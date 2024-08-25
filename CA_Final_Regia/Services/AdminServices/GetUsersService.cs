using CA_Final_Regia.DTOs;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;
namespace CA_Final_Regia.Services.AdminServices
{
    public class GetUsersService(IAccountRepository accountRepository) : IGetUsersService
    {
        public async Task<ResponseDto<AccountDto>> GetUsersAsync()
        {
            var accounts = await accountRepository.GetAllAccountsAsync();
            if (!accounts.Any())
            {
                return new ResponseDto<AccountDto>(false, "No acconts found", ResponseDto<AccountDto>.Status.Not_Found);
            }
            var accountsDto = accounts.Select(account => new AccountDto
            {
                AccountId = account.AccountId,
                UserName = account.UserName,
                Role = account.Role
            }).ToList();
            return new ResponseDto<AccountDto>(true, accountsDto, ResponseDto<AccountDto>.Status.Ok);
        }
    }
}
