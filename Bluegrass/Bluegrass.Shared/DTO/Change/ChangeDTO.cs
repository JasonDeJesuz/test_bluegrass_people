using System;
namespace Bluegrass.Shared.DTO.Change
{
    public class ChangeDTO
    {
        public string Class { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}