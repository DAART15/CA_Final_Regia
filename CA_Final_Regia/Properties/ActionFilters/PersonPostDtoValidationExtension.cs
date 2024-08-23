using CA_Final_Regia.DTOs;
using System.Text.RegularExpressions;

namespace CA_Final_Regia.Properties.ActionFilters
{
    public static partial class PersonPostDtoValidationExtension
    {
        public static ResponseDto<PersonPostDto> ValidatePersonInfo(this PersonPostDto personPostDto)
        {
            if (personPostDto == null)
            {
                return new ResponseDto<PersonPostDto>(false, "Person object is null", ResponseDto<PersonPostDto>.Status.Bad_Request);
            }
            if (string.IsNullOrWhiteSpace(personPostDto.FirstName))
            {
                return new ResponseDto<PersonPostDto>(false, "First name is required", ResponseDto<PersonPostDto>.Status.Bad_Request);
            }
            if (string.IsNullOrWhiteSpace(personPostDto.LastName))
            {
                return new ResponseDto<PersonPostDto>(false, "Last name is required", ResponseDto<PersonPostDto>.Status.Bad_Request);
            }
            if (!PersonalIdRegex().IsMatch(personPostDto.PersonalId.ToString().Trim()))
            {
                
                return new ResponseDto<PersonPostDto>(false, "Personal ID is required consist of 11 digits", ResponseDto<PersonPostDto>.Status.Bad_Request);
            }
            if (!PhoneNumberRegex().IsMatch(personPostDto.PhoneNumber.Trim()))
            {
                return new ResponseDto<PersonPostDto>(false, "Phone number is required consist of 5-15 digits", ResponseDto<PersonPostDto>.Status.Bad_Request);
            }
            if (!EmailRegex().IsMatch(personPostDto.Mail))
            {
                return new ResponseDto<PersonPostDto>(false, "Valid Mail is required", ResponseDto<PersonPostDto>.Status.Bad_Request);
            }
            if (personPostDto.Image == null)
            {
                return new ResponseDto<PersonPostDto>(false, "Image is required", ResponseDto<PersonPostDto>.Status.Bad_Request);
            }
            return new ResponseDto<PersonPostDto>(true, "Person info is valid", ResponseDto<PersonPostDto>.Status.Ok);
        }

        [GeneratedRegex(@"^[0-9]{11}$")]
        private static partial Regex PersonalIdRegex();
        [GeneratedRegex(@"^[0-9]{5,15}$")]
        private static partial Regex PhoneNumberRegex();
        [GeneratedRegex(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}")]
        private static partial Regex EmailRegex();

    }
}
