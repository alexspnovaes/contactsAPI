using System;
using System.Linq;
using TechChallenge.Infrastructure.Data;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Contacts.Any())
            {
                return; 
            }

            var contacts = new Contact[]
            {
                new Contact { Id = Guid.NewGuid(), Name = "John Doe", PhoneNumber = "12345678", Email = "johndoe@example.com", Ddd = "011" },
                new Contact { Id = Guid.NewGuid(), Name = "Jane Smith", PhoneNumber = "87654321", Email = "janesmith@example.com", Ddd = "012" }
            };

            context.Contacts.AddRange(contacts);
            context.SaveChanges();
        }
    }
}
