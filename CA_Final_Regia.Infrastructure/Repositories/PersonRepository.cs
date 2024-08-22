using CA_Final_Regia.Domain.Models;
using CA_Final_Regia.Infrastructure.DataBase;
using CA_Final_Regia.Infrastructure.Interfaces;
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
        public async Task<Person> CreatePersonAsync(Person person)
        {
            try
            {
                await _dbContext.Persons.AddAsync(person);
                await _dbContext.SaveChangesAsync();
                return person;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
