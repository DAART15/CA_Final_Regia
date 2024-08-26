using CA_Final_Regia.Domain.Entities;
namespace CA_Final_Regia.Domain.Interfaces.Repository
{
    public interface IPersonRepository
    {
        Task<Person?> GetPersonAsync(Guid accountId);
        Task CreatePersonAsync(Person person);
        Task UpdatePersonAsync(Person person);
    }
}
