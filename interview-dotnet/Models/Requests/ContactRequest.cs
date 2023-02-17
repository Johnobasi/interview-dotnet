using System.ComponentModel.DataAnnotations;

namespace interview_dotnet.Models.Requests
{
    public class ContactRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return $"FirstName:{FirstName} LastName: {LastName}PhoneNumber : {PhoneNumber}";
        }
    }
}
