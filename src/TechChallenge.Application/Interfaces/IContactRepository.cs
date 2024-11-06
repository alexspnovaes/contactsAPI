using TechChallenge.Domain.Entities;

namespace TechChallenge.Application.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();        
        Task<Contact> GetByIdAsync(Guid id);             
        Task<Contact> GetByNameAsync(string name);       
        Task<IEnumerable<Contact>> GetByDddAsync(string ddd);
        Task AddAsync(Contact contact);                  
        Task UpdateAsync(Contact contact);               
        Task DeleteAsync(Contact contact);
    }
}
