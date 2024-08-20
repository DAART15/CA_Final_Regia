using CA_Final_Regia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_Final_Regia.Infrastructure.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountAsync(string userName);
        Task<Account> CreateAccountAsync(Account account);
        Task DeleteAccountAsync(Account account);
    }
}
