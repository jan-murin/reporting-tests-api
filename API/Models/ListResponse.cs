using System.Collections.Generic;

namespace Reporting.Tests.API.API.Models
{
    public class ListResponse
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<UserModel> data { get; set; }
    }
}
