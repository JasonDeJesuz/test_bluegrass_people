using System;

namespace Bluegrass.Shared.DTO.Generic
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public Object Data { get; set; }
        public string Message { get; set; }
    }
}