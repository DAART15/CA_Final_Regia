using CA_Final_Regia.DTOs;
using System.Text.RegularExpressions;

namespace CA_Final_Regia.Properties.ActionFilters
{
    public static partial class DtoKeyValueValidationExtension
    {
        //PersonPostDto properties
        private const string FirstName = "FirstName";
        private const string LastName = "LastName";
        private const string PersonalId = "PersonalId";
        private const string PhoneNumber = "PhoneNumber";
        private const string Mail = "Mail";
        private const string Image = "Image";
        private const string PersonalIdRegex = @"^[0-9]{11}$";
        private const string PhoneNumberRegex = @"^[0-9]{5,15}$";
        private const string EmailRegex = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        private const string ImageExtension = ".jpg";
        //LocationDto properties
        private const string City = "City";
        private const string Street = "Street";
        private const string HouseNr = "HouseNr";
        private const string ApartmentNr = "ApartmentNr";

        public static ResponseDto<T> ValidateKeyValue<T>(this KeyValue keyValue) where T : class
        {
            if (keyValue == null)
            {
                return new ResponseDto<T>(false, "Object is null", ResponseDto<T>.Status.Bad_Request);
            }
            if (string.IsNullOrWhiteSpace(keyValue.Key))
            {
                return new ResponseDto<T>(false, "Key is required", ResponseDto<T>.Status.Bad_Request);
            }
            if (keyValue.Value == null)
            {
                return new ResponseDto<T>(false, "Value is required", ResponseDto<T>.Status.Bad_Request);
            }
            string valueAsString = keyValue.Value.ToString();
            switch (keyValue.Key)
            {
                case FirstName:
                    if (string.IsNullOrWhiteSpace(valueAsString))
                    {
                        return new ResponseDto<T>(false, "First name is required", ResponseDto<T>.Status.Bad_Request);
                    }
                    if (valueAsString.Length > 150)
                    {
                        return new ResponseDto<T>(false, "First name is too long", ResponseDto<T>.Status.Bad_Request);
                    }
                    break;
                case LastName:
                    if (string.IsNullOrWhiteSpace(valueAsString))
                    {
                        return new ResponseDto<T>(false, "Last name is required", ResponseDto<T>.Status.Bad_Request);
                    }
                    if (valueAsString.Length > 150)
                    {
                        return new ResponseDto<T>(false, "Last name is too long", ResponseDto<T>.Status.Bad_Request);
                    }
                    break;
                case PersonalId:
                    if (!Regex.IsMatch(PersonalIdRegex, valueAsString))
                    {

                        return new ResponseDto<T>(false, "Personal ID is required consist of 11 digits", ResponseDto<T>.Status.Bad_Request);
                    }
                    break;
                case PhoneNumber:
                    if (!Regex.IsMatch(PhoneNumberRegex, valueAsString))
                    {
                        return new ResponseDto<T>(false, "Phone number is required consist of 5-15 digits", ResponseDto<T>.Status.Bad_Request);
                    }
                    break;
                case Mail:
                    if (!Regex.IsMatch(EmailRegex, valueAsString))
                    {
                        return new ResponseDto<T>(false, "Valid Mail is required", ResponseDto<T>.Status.Bad_Request);
                    }
                    break;
                case Image:
                    if (keyValue.Value is not IFormFile picture)
                    {
                        return new ResponseDto<T>(false, "Invalid file data.", ResponseDto<T>.Status.Bad_Request);
                    }

                    if (!IsAllowedExtension(picture, ImageExtension))
                    {
                        return new ResponseDto<T>(false, "Invalid file extension. Only .jpg files are allowed.", ResponseDto<T>.Status.Bad_Request);
                    }
                    break;
                case City:
                    if (string.IsNullOrWhiteSpace(valueAsString))
                    {
                        return new ResponseDto<T>(false, "City is required", ResponseDto<T>.Status.Bad_Request);
                    }
                    if (valueAsString.Length > 100)
                    {
                        return new ResponseDto<T>(false, "City name is too long", ResponseDto<T>.Status.Bad_Request);
                    }
                    break;
                case Street:
                    if (string.IsNullOrWhiteSpace(valueAsString))
                    {
                        return new ResponseDto<T>(false, "Street is required", ResponseDto<T>.Status.Bad_Request);
                    }
                    if (valueAsString.Length > 100)
                    {
                        return new ResponseDto<T>(false, "Street name is too long", ResponseDto<T>.Status.Bad_Request);
                    }
                    break;
                case HouseNr:
                    if (string.IsNullOrWhiteSpace(valueAsString))
                    {
                        return new ResponseDto<T>(false, "House number is required", ResponseDto<T>.Status.Bad_Request);
                    }
                    if (valueAsString.Length > 5)
                    {
                        return new ResponseDto<T>(false, "House number is too long", ResponseDto<T>.Status.Bad_Request);
                    }
                    break;
                case ApartmentNr:
                    if (string.IsNullOrWhiteSpace(valueAsString))
                    {
                        return new ResponseDto<T>(false, "Apartment number is required", ResponseDto<T>.Status.Bad_Request);
                    }
                    if (valueAsString.Length > 4)
                    {
                        return new ResponseDto<T>(false, "Apartment number is too long", ResponseDto<T>.Status.Bad_Request);
                    }
                    break;
                default:
                    return new ResponseDto<T>(false, "Bad Key", ResponseDto<T>.Status.Bad_Request);
            }
            return new ResponseDto<T>(true, "Object info is valid", ResponseDto<T>.Status.Ok);
        }
        private static bool IsAllowedExtension(IFormFile file, string extension)
        {
            return file.FileName.ToLowerInvariant().EndsWith(extension);
        }
    }
}
