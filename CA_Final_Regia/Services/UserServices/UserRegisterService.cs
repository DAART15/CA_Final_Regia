using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.DTOs;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CA_Final_Regia.Services.UserServices
{
    public class UserRegisterService(IAccountRepository accountRepository) : IUserRegisterService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        public async Task<ResponseDto<Account>> RegisterAsync(string username, string password)
        {
            try
            {
                if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]{6,12}$"))
                {
                    return new ResponseDto<Account>(false, "Nickname is not valid. Requaements: at least 6 characters, at most 12 characters only letters (both uppercase and lowercase), digits, and underscores", ResponseDto<Account>.Status.Bad_Request);
                }
                if (!Regex.IsMatch(password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W)[\w\W]{8,}$"))
                {
                    return new ResponseDto<Account>(false, "Password is not valid. Requaements: at least one digit, one lowercase letter, one uppercase letter, one special character, at least 8 characters", ResponseDto<Account>.Status.Bad_Request);
                }
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                Account account = new Account
                {
                    UserName = username,
                    Password = passwordHash,
                    Salt = passwordSalt,
                    Role = "User"
                };
                var acc = await _accountRepository.CreateAccountAsync(account);
                if (acc == null)
                {
                    return new ResponseDto<Account>(false, "Somemeting went wrong. try again", ResponseDto<Account>.Status.Not_Found);
                }
                return new ResponseDto<Account>(true, "Account for User created successfully", ResponseDto<Account>.Status.Created);
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
