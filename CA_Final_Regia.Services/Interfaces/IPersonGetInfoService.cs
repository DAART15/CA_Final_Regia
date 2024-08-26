using CA_Final_Regia.Services.DTOs;
namespace CA_Final_Regia.Services.Interfaces
{
    public interface IPersonGetInfoService
    {
        Task<ResponseDto<PersonGetDto>> GetPersonInfoAsync(Guid accountId);
    }
}
