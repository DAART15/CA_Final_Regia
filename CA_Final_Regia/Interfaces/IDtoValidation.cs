using CA_Final_Regia.DTOs;

namespace CA_Final_Regia.Interfaces
{
    public interface IDtoValidation<T> where T : class
    {
        ResponseDto<T> DtoKeyValueValidation(T dto);
    }
}
