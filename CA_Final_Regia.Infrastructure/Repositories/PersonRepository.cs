using CA_Final_Regia.Domain.Entities;
using CA_Final_Regia.Domain.Interfaces.Repository;
using CA_Final_Regia.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
namespace CA_Final_Regia.Infrastructure.Repositories
{
    public class PersonRepository(AplicationDbContext dbContext) : IPersonRepository
    {
        private readonly AplicationDbContext _dbContext = dbContext;

        public async Task<Person?> GetPersonAsync(Guid accountId)
        {
            try
            {
                return await _dbContext.Persons.FirstOrDefaultAsync(p => p.AccountId == accountId);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task CreatePersonAsync(Person person)
        {
            try
            {
                await _dbContext.Persons.AddAsync(person);
                await _dbContext.SaveChangesAsync();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task UpdatePersonAsync(Person person)
        {
            try
            {
                _dbContext.Persons.Update(person);
                await _dbContext.SaveChangesAsync();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
