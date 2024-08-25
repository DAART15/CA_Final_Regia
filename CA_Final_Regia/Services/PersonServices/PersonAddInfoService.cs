using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.DTOs;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;
namespace CA_Final_Regia.Services.PersonServices
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
                var response = await personRepository.CreatePersonAsync(person);
                if (response == null)
                {
                    return new ResponseDto<PersonPostDto>(false, "Person not added", ResponseDto<PersonPostDto>.Status.Internal_Server_Error);
                }
                return new ResponseDto<PersonPostDto>(true, "Person added", ResponseDto<PersonPostDto>.Status.Created);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
