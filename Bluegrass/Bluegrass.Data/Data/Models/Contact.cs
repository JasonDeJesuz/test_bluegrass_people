using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bluegrass.Data.Data.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        
        public virtual Person? Person { get; set; }
    }
}