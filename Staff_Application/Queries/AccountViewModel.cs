using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Application.Queries
{
    public record AccountViewModel
    {
        public string username { get; init; }
        public Guid accountId { get; init; }
        public int agencyId { get; init; }
        public string name { get; init; }
        public string email { get; init; }
        public int mobileNo { get; init; }

    }
}
