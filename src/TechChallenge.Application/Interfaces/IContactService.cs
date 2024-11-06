using TechChallenge.Application.Models;

namespace TechChallenge.Application.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactModel>> GetAllContactsAsync();
        Task<ContactModel> GetContactByIdAsync(Guid id);
        Task<ContactModel> GetContactByNameAsync(string name);
        Task<IEnumerable<ContactModel>> GetContactByDddAsync(string ddd);
        Task<ContactModel> CreateContactAsync(ContactModel contactDto);
        Task<ContactModel> UpdateContactAsync(Guid id, ContactModel contactDto);
        Task<bool> DeleteContactAsync(Guid id);
    }
}
