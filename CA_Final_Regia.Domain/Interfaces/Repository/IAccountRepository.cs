using CA_Final_Regia.Domain.Entities;
namespace CA_Final_Regia.Domain.Interfaces.Repository
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountByUserNameAsync(string userName);
        Task<Account?> GetAccountByIdAsync(Guid accountId);
        Task CreateAccountAsync(Account account);
        Task DeleteAccountAsync(Account account);
        Task<List<Account>> GetAllAccountsAsync();
    }
}
