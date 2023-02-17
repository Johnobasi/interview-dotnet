using interview_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace interview_dotnet.Context
{
    public class ContactsContext : DbContext
    {

        public DbSet<Contact> Contacts { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Contacts");
        }
    }
}
