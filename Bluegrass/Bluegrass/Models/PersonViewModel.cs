using Bluegrass.Shared.DTO.Person;

namespace Bluegrass.Models
{
    public class PersonViewModel
    {
        public string? ActionType { get; set; }
        public int? Id { get; set; }
        public PersonDTO? Person { get; set; }
    }
}
