using CA_Final_Regia.Services.DTOs;
namespace CA_Final_Regia.Services.Interfaces
{
    public interface IDtoValidation<T> where T : class
    {
        ResponseDto<T> DtoKeyValueValidation(T dto);
    }
}
