using System;

namespace Reporting.Tests.API.API.Models
{
    public class CreateUserResponse
    {
        public string name { get; set; }
        public string job { get; set; }
        public string id { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
