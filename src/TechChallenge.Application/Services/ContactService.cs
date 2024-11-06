using TechChallenge.Application.Interfaces;
using TechChallenge.Application.Models;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<ContactModel>> GetAllContactsAsync()
        {
            var contacts = await _contactRepository.GetAllAsync();
            return contacts.Select(c => new ContactModel
            {
                Id = c.Id,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                Ddd = c.Ddd
            });
        }

        public async Task<ContactModel> CreateContactAsync(ContactModel contactModel)
        {
            var contact = new Contact
            {
                Name = contactModel.Name,
                PhoneNumber = contactModel.PhoneNumber,
                Email = contactModel.Email,
                Ddd = contactModel.Ddd
            };

            await _contactRepository.AddAsync(contact);
            return contactModel;
        }

        public async Task<ContactModel> UpdateContactAsync(Guid id, ContactModel contactModel)
        {
            var contact = await _contactRepository.GetByIdAsync(id);

            if (contact == null)
            {
                throw new Exception("Contact not found");
            }

            contact.Name = contactModel.Name;
            contact.PhoneNumber = contactModel.PhoneNumber;
            contact.Email = contactModel.Email;
            contact.Ddd = contactModel.Ddd;

            await _contactRepository.UpdateAsync(contact);

            return new ContactModel
            {
                Id = contact.Id,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                Ddd = contact.Ddd
            };
        }

        public async Task<ContactModel> GetContactByIdAsync(Guid id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            return contact == null
                ? throw new Exception("Contact not found")
                : new ContactModel
            {
                Id = contact.Id,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                Ddd = contact.Ddd
            };
        }

        public async Task<ContactModel> GetContactByNameAsync(string name)
        {
            Contact contact = await _contactRepository.GetByNameAsync(name);
            return contact == null
                ? throw new Exception("Contact not found")
                : new ContactModel
                {
                Id = contact.Id,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                Ddd = contact.Ddd
            };
        }

        public async Task<IEnumerable<ContactModel>> GetContactByDddAsync(string ddd)
        {
            var contacts = await _contactRepository.GetByDddAsync(ddd);
            return contacts == null
               ? throw new Exception("Contact not found")
               : contacts.Select(c => new ContactModel
               {
                   Id = c.Id,
                   Name = c.Name,
                   PhoneNumber = c.PhoneNumber,
                   Email = c.Email,
                   Ddd = c.Ddd
               });
        }

        public async Task<bool> DeleteContactAsync(Guid id)
        {
            var contact = await _contactRepository.GetByIdAsync(id) ?? throw new Exception("Contact not found");
            await _contactRepository.DeleteAsync(contact);
            return true;
        }
    }
}
