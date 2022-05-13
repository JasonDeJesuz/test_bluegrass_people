using System;
using Bluegrass.Shared.DTO.Person;

namespace Bluegrass.Shared.DTO.ProfilePicture
{
    public class ProfilePictureDTO
    {
        public int Id { get; set; }
        public byte[] Bytes { get; set; }
        public string Description { get; set; }
        public string FileExtension { get; set; }
        public decimal Size { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}