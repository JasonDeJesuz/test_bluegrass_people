using Bluegrass.Shared.DTO.Person;

namespace Bluegrass.Shared.DTO.Contact
{
    public class ContactDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        
        public virtual PersonDTO? Person { get; set; }
    }
}