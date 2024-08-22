using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.Infrastructure.DataBase;
using CA_Final_Regia.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CA_Final_Regia.Infrastructure.Repositories
{
    public class AccountRepository(AplicationDbContext dbContext) : IAccountRepository
    {
        private readonly AplicationDbContext _dbContext = dbContext;

        public async Task<Account?> GetAccountByUserNameAsync(string userName)
        {
            try
            {
                return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.UserName == userName);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Account?> GetAccountByIdAsync(Guid accountId)
        {
            try
            {
                return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Account> CreateAccountAsync(Account account)
        {
            try
            {
                await _dbContext.Accounts.AddAsync(account);
                await _dbContext.SaveChangesAsync();
                return account;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task DeleteAccountAsync(Account account)
        {
            try
            {
                _dbContext.Accounts.Remove(account);
                await _dbContext.SaveChangesAsync();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<List<Account>> GetAllAccountsAsync()
        {
            try
            {
                return await _dbContext.Accounts.ToListAsync();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
