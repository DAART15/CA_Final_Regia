using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace CA_Final_Regia.Services.UserServices
{
    public class UserLogInService(IAccountRepository accountRepository) : IUserLogInService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        public async Task LogIn(string username, string password)
        {
            var acc = await _accountRepository.GetAccountAsync(username);

            if (acc == null)
            {
                return false;
            }

            if (VerifyPasswordHash(password, acc.Password, acc.Salt))
            {
                return true;
            }

            return false;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return hash.SequenceEqual(passwordHash);

        }
    }
}
