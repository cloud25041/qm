using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Application.Queries
{
    public record AgencyViewModel
    {
        public int AgencyId { get; init; }
        public string AgencyPin { get; init; }
        public string AgencyName { get; init; }
        public string Location { get; init; }
        public int VirtualConcurrentUser { get; init; }
        public int PhysicalConcurrentUser { get; init; }

    }
}
