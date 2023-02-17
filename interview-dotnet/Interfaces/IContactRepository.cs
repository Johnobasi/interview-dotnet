using interview_dotnet.Models.Requests;

namespace interview_dotnet.Interfaces
{
    public interface IContactRepository
    {
        Task AddContact(ContactRequest contactRequest);
    }
}
