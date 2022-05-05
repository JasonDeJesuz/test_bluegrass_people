using System;
using System.ComponentModel.DataAnnotations;

namespace Bluegrass.Data.Data.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
        public ProfilePicture ProfilePicture { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}