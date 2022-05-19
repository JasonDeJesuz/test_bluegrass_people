using System;

namespace Bluegrass.Shared.DTO.Helper
{
    public class DropdownCountryDTO
    {
        public int Id { get; set; }
        public string? Country { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}