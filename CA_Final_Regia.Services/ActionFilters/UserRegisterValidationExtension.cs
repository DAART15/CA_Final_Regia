using CA_Final_Regia.Services.DTOs;
using System.Text.RegularExpressions;
namespace CA_Final_Regia.Services.ActionFilters
{
    public static partial class UserRegisterValidationExtension
    {
        public static ResponseDto<User> ValidateUserRegister(this User user)
        {
            if (user == null)
            {
                return new ResponseDto<User>(false, "User object is null", ResponseDto<User>.Status.Bad_Request);
            }
            if (!UserNameRegex().IsMatch(user.UserName))
            {
                return new ResponseDto<User>(false, "Nickname is not valid. Requaements: at least 6 characters, at most 12 characters only letters (both uppercase and lowercase), digits, and underscores", ResponseDto<User>.Status.Bad_Request);
            }
            if (!PasswordRegex().IsMatch(user.Password))
            {
                return new ResponseDto<User>(false, "Password is not valid. Requaements: at least one digit, one lowercase letter, one uppercase letter, one special character, at least 8 characters", ResponseDto<User>.Status.Bad_Request);
            }
            return new ResponseDto<User>(true, "User info is valid", ResponseDto<User>.Status.Ok);
        }

        [GeneratedRegex(@"^[a-zA-Z0-9_]{6,12}$")]
        private static partial Regex UserNameRegex();
        [GeneratedRegex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W)[\w\W]{8,}$")]
        private static partial Regex PasswordRegex();
    }
}
