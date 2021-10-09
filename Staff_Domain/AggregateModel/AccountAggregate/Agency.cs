using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Domain.AggregateModel.AccountAggregate
{
    public class Agency
    {
        public Agency(Guid agencyId, string agencyPin, string location, int virtualConcurrentUser, int physicalConcurrentUser)
        {
            AgencyId = agencyId;
            AgencyPin = agencyPin;
            Location = location;
            VirtualConcurrentUser = virtualConcurrentUser;
            PhysicalConcurrentUser = physicalConcurrentUser;
        }

        public Guid AgencyId { get; private set; }
        public string AgencyPin { get; private set; }
        public string Location { get; private set; }
        public int VirtualConcurrentUser { get; private set; }
        public int PhysicalConcurrentUser { get; private set; }
    }
}
