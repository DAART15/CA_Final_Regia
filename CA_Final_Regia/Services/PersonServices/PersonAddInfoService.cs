using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.DTOs;
using CA_Final_Regia.Infrastructure.Interfaces;
using CA_Final_Regia.Interfaces;
namespace CA_Final_Regia.Services.PersonServices
{
    public class PersonAddInfoService(IPersonRepository personRepository, IPictureResizeService pictureResizeService) : IPersonAddInfoService
    {
        private readonly IPersonRepository _personRepository = personRepository;
        private readonly IPictureResizeService _pictureResizeService = pictureResizeService;
                
        public async Task<ResponseDto<PersonPostDto>> AddPersonInfoAsync(PersonPostDto personPostDto, Guid accountId)
        {
            try
            {
                if (personPostDto == null)
                { 
                    return new ResponseDto<PersonPostDto>(false, "Person not added", ResponseDto<PersonPostDto>.Status.Bad_Request); 
                }
                PictureDto picture = new PictureDto { Image = personPostDto.Image };
                var imageBytes = await _pictureResizeService.ResizePictureAsync(picture);
                Person person = new Person
                {
                    AccountId = accountId,
                    FirstName = personPostDto.FirstName,
                    LastName = personPostDto.LastName,
                    PersonalId = personPostDto.PersonalId,
                    PhoneNumber = personPostDto.PhoneNumber,
                    Mail = personPostDto.Mail,
                    FileData = imageBytes,
                };               
                var response = await _personRepository.CreatePersonAsync(person);
                if (response == null)
                {
                    return new ResponseDto<PersonPostDto>(false, "Person not added", ResponseDto<PersonPostDto>.Status.Not_Found);
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
