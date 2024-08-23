using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.DTOs;
namespace CA_Final_Regia.Interfaces
{
    public interface IPersonAddInfoService
    {
        Task<ResponseDto<PersonPostDto>> AddPersonInfoAsync(PersonPostDto personDto, Guid accountId);
    }
}