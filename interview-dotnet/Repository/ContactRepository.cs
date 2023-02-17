using interview_dotnet.Context;
using interview_dotnet.Interfaces;
using interview_dotnet.Models;
using interview_dotnet.Models.Requests;

namespace interview_dotnet.Repository
{
    public class ContactRepository : IContactRepository
    {

        private readonly ContactsContext _contactDbContext;

        public ContactRepository(ContactsContext contactDbContext)
        {
            _contactDbContext = contactDbContext;
        }


        public async Task AddContact(ContactRequest contactRequest)
        {
            _contactDbContext.Contacts.Add(new Contact
            {
                Id = Guid.NewGuid(),
                FirstName = contactRequest.FirstName,
                LastName = contactRequest.LastName,
                PhoneNumber = contactRequest.PhoneNumber
            });

            await _contactDbContext.SaveChangesAsync();
        }
    }
}
