using Microsoft.EntityFrameworkCore;
using TechChallenge.Application.Interfaces;
using TechChallenge.Domain.Entities;
using TechChallenge.Infrastructure.Data;

namespace TechChallenge.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Contact>> GetAllAsync() => await _context.Contacts.ToListAsync();

        public async Task<Contact> GetByIdAsync(Guid id) => await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Contact> GetByNameAsync(string name) => await _context.Contacts.FirstOrDefaultAsync(c => c.Name == name);

        public async Task<IEnumerable<Contact>> GetByDddAsync(string ddd) => await _context.Contacts.Where(c => c.Ddd == ddd).ToListAsync();

        public async Task AddAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Contact contact)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }
}
