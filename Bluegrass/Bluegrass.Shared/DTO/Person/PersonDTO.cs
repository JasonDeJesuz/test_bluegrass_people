using Bluegrass.Shared.DTO.Address;
using Bluegrass.Shared.DTO.Contact;
using Bluegrass.Shared.DTO.ProfilePicture;

namespace Bluegrass.Shared.DTO.Person
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public AddressDTO Address { get; set; }
        public ContactDTO Contact { get; set; }
        public ProfilePictureDTO ProfilePicture { get; set; }
    }
}