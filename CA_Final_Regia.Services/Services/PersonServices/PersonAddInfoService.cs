using CA_Final_Regia.Domain.Entities;
using CA_Final_Regia.Domain.Interfaces.Repository;
using CA_Final_Regia.Services.DTOs;
using CA_Final_Regia.Services.Interfaces;
namespace CA_Final_Regia.Services.Services.PersonServices
{
    public class PersonAddInfoService(IPersonRepository personRepository, IPictureResizeService pictureResizeService, IDtoValidation<PersonPostDto> dtoValidation) : IPersonAddInfoService
    {
        public async Task<ResponseDto<PersonPostDto>> AddPersonInfoAsync(PersonPostDto personPostDto, Guid accountId)
        {
            try
            {
                var validation = dtoValidation.DtoKeyValueValidation(personPostDto);
                if (!validation.IsSuccess)
                {
                    return new ResponseDto<PersonPostDto>(false, validation.Message, ResponseDto<PersonPostDto>.Status.Bad_Request);
                }
                PictureDto picture = new() { Image = personPostDto.Image };
                var imageBytes = await pictureResizeService.ResizePictureAsync(picture);
                Person person = new()
                {
                    AccountId = accountId,
                    FirstName = personPostDto.FirstName,
                    LastName = personPostDto.LastName,
                    PersonalId = personPostDto.PersonalId,
                    PhoneNumber = personPostDto.PhoneNumber,
                    Mail = personPostDto.Mail,
                    FileData = imageBytes,
                };
                await personRepository.CreatePersonAsync(person);
                return new ResponseDto<PersonPostDto>(true, "Person added", ResponseDto<PersonPostDto>.Status.Created);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
