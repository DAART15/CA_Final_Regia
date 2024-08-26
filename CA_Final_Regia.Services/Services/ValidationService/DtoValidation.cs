using CA_Final_Regia.Services.ActionFilters;
using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
namespace CA_Final_Regia.Services.Services.ValidationService
{
    public class DtoValidation<T> : IDtoValidation<T> where T : class
    {
        public ResponseDto<T> DtoKeyValueValidation(T dto)
        {
            foreach (var keyValuePair in dto.GetType().GetProperties())
            {
                var key = keyValuePair.Name;
                var value = keyValuePair.GetValue(dto);
                var keyValue = new KeyValue { Key = key, Value = value };
                var validation = keyValue.ValidateKeyValue<T>();
                if (!validation.IsSuccess)
                {
                    return new ResponseDto<T>(false, validation.Message, ResponseDto<T>.Status.Bad_Request);
                }
            }
            return new ResponseDto<T>(true, "Dto Object info is valid", ResponseDto<T>.Status.Ok);
        }
    }
}
