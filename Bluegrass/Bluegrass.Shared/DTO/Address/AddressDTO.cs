using Bluegrass.Shared.DTO.Person;

namespace Bluegrass.Shared.DTO.Address
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        
        public virtual PersonDTO? Person { get; set; }
    }
}