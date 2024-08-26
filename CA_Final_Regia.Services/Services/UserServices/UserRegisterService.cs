using CA_Final_Regia.Domain.Entities;
using CA_Final_Regia.Domain.Interfaces.Repository;
using CA_Final_Regia.Services.ActionFilters;
using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;
namespace CA_Final_Regia.Services.Services.UserServices
{
    public class UserRegisterService(IAccountRepository accountRepository) : IUserRegisterService
    {
        public async Task<ResponseDto<User>> RegisterAsync(User user)
        {
            try
            {
                var validation = user.ValidateUserRegister();
                if (!validation.IsSuccess)
                {
                    return new ResponseDto<User>(false, validation.Message, ResponseDto<User>.Status.Bad_Request);
                }
                CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                Account account = new Account
                {
                    UserName = user.UserName,
                    Password = passwordHash,
                    Salt = passwordSalt,
                    Role = "User"
                };
                await accountRepository.CreateAccountAsync(account);
                return new ResponseDto<User>(true, "Account for User created successfully", ResponseDto<User>.Status.Created);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
