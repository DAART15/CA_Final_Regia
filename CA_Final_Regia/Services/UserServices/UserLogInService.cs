using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.DTOs;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace CA_Final_Regia.Services.UserServices
{
    public class UserLogInService(IAccountRepository accountRepository) : IUserLogInService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        public async Task<ResponseDto<Account>> LogInAsync(string username, string password)
        {
            try
            {
                var acc = await _accountRepository.GetAccountAsync(username);
                if (acc == null)
                {
                    return new ResponseDto<Account>(false, "User not found", ResponseDto<Account>.Status.Not_Found);
                }
                if (VerifyPasswordHash(password, acc.Password, acc.Salt))
                {
                    return new ResponseDto<Account>(true, "Connected successfully", ResponseDto<Account>.Status.Ok, acc.Role, acc.AccountId.ToString());
                }
                return new ResponseDto<Account>(false, "Bad password", ResponseDto<Account>.Status.Not_Found);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return hash.SequenceEqual(passwordHash);

        }
    }
}
