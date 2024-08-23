using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.DTOs;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;
using CA_Final_Regia.Properties.ActionFilters;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CA_Final_Regia.Services.UserServices
{
    public class UserRegisterService(IAccountRepository accountRepository) : IUserRegisterService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        public async Task<ResponseDto<User>> RegisterAsync(User user)
        {
            try
            {
                var userValidation = user.ValidateUserRegister();
                if (!userValidation.IsSuccess)
                {
                    return new ResponseDto<User>(false, userValidation.Message, ResponseDto<User>.Status.Bad_Request);
                }
                CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
                Account account = new Account
                {
                    UserName = user.UserName,
                    Password = passwordHash,
                    Salt = passwordSalt,
                    Role = "User"
                };
                var acc = await _accountRepository.CreateAccountAsync(account);
                if (acc == null)
                {
                    return new ResponseDto<User>(false, "Somemeting went wrong. try again", ResponseDto<User>.Status.Not_Found);
                }
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
