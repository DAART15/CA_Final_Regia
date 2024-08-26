using CA_Final_Regia.Services.DTOs;
namespace CA_Final_Regia.Services.Interfaces
{
    public interface IPersonAddInfoService
    {
        Task<ResponseDto<PersonPostDto>> AddPersonInfoAsync(PersonPostDto personDto, Guid accountId);
    }
}