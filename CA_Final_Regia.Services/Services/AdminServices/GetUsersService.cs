using CA_Final_Regia.Domain.Interfaces.Repository;
using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
namespace CA_Final_Regia.Services.Services.AdminServices
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
