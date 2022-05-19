using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bluegrass.Data.Data.Models
{
    public class DropdownCountry
    {
        [Key]
        public int Id { get; set; }
        public string? Country { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}