using CA_Final_Regia.Domain.Models;
namespace CA_Final_Regia.Infrastructure.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person?> GetPersonAsync(Guid accountId);
        Task<Person> CreatePersonAsync(Person person);
        Task DeletePersonAsync(Person person);
    }
}
