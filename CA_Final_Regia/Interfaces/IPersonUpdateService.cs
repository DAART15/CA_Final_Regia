using CA_Final_Regia.DTOs;

namespace CA_Final_Regia.Interfaces
{
    public interface IPersonUpdateService
    {
        Task<ResponseDto<PersonPostDto>> UpdatePersonAsync(KeyValue personUpdateKeyValue, Guid accountId);
    }
}
