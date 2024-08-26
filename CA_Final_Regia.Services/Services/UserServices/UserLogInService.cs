using CA_Final_Regia.Domain.Entities;
using CA_Final_Regia.Domain.Interfaces.Repository;
using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;
namespace CA_Final_Regia.Services.Services.UserServices
{
    public class UserLogInService(IAccountRepository accountRepository) : IUserLogInService
    {
        public async Task<ResponseDto<Account>> LogInAsync(User user)
        {
            try
            {
                var acc = await accountRepository.GetAccountByUserNameAsync(user.UserName);
                if (acc == null)
                {
                    return new ResponseDto<Account>(false, "User not found", ResponseDto<Account>.Status.Not_Found);
                }
                if (VerifyPasswordHash(user.Password, acc.Password, acc.Salt))
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
