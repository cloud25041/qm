using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AR_Application.Queries
{
    public record AgencyViewModel
    {
        public string AgencyName { get; init; }
        public int AgencyId { get; init; }
        public int AgencyPin { get; init; }
        public string Location { get; init; }
        public int PhysicalConcurrentUser { get; init; }
        public int VirtualConcurrentUser { get; init; }
    }
}
