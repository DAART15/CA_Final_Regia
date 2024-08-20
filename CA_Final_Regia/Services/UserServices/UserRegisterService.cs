using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace CA_Final_Regia.Services.UserServices
{
    public class UserRegisterService(IAccountRepository accountRepository) : IUserRegisterService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        public async Task Register(string username, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            Account account = new Account
            {
                UserName = username,
                Password = passwordHash,
                Salt = passwordSalt,
                Role = "User"
            };
            var acc = await _accountRepository.CreateAccountAsync(account);

        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
