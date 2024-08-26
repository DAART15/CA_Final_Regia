using CA_Final_Regia.Services.DTOs;
namespace CA_Final_Regia.Services.Interfaces
{
    public interface IPersonUpdateService
    {
        Task<ResponseDto<PersonPostDto>> UpdatePersonAsync(KeyValue personUpdateKeyValue, Guid accountId);
        Task<ResponseDto<PictureDto>> UpdatePersonPictureAsync(PictureDto pictureDto, Guid accountId);
    }
}
