using System;
using Bluegrass.Shared.DTO.Person;

namespace Bluegrass.Shared.DTO.Address
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public int PersonId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public AddressDTO()
        {
            DateCreated = DateTime.Now;
        }
    }
}