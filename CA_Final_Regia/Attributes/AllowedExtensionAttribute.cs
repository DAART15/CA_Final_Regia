using System.ComponentModel.DataAnnotations;

namespace CA_Final_Regia.Attributes
{
    public class AllowedExtensionAttribute(string[] _extensions) : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult($"This photo extension is not allowed. Allowed extensions are {string.Join(", ", _extensions)}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
