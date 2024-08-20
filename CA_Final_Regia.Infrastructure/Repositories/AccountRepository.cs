using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.Infrastructure.DataBase;
using CA_Final_Regia.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CA_Final_Regia.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AplicationDbContext _dbContext;

        public AccountRepository(AplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Account> GetAccountAsync(string userName)
        {
            return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.UserName == userName);
        }
        public async Task<Account> CreateAccountAsync(Account account)
        {
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
            return account;
        }
        public async Task DeleteAccountAsync(Account account)
        {
            _dbContext.Accounts.Remove(account);
            await _dbContext.SaveChangesAsync();
        }
    }
}
