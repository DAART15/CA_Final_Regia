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
                
        public async Task<ResponseDto<Person>> AddPersonInfoAsync(PersonDto personDto, Guid accountId)
        {
            try
            {
                if (personDto == null)
                { 
                    return new ResponseDto<Person>(false, "Person not added", ResponseDto<Person>.Status.Bad_Request); 
                }
                PictureDto picture = new PictureDto { Image = personDto.Image };
                var imageBytes = await _pictureResizeService.ResizePictureAsync(picture);
                Person person = new Person
                {
                    AccountId = accountId,
                    FirstName = personDto.FirstName,
                    LastName = personDto.LastName,
                    PersonalId = personDto.PersonalId,
                    PhoneNumber = personDto.PhoneNumber,
                    Mail = personDto.Mail,
                    FileData = imageBytes,
                };                var response = await _personRepository.CreatePersonAsync(person);
                if (response == null)
                {
                    return new ResponseDto<Person>(false, "Person not added", ResponseDto<Person>.Status.Not_Found);
                }
                return new ResponseDto<Person>(true, "Person added", ResponseDto<Person>.Status.Ok);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
