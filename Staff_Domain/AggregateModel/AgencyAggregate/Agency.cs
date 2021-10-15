using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Domain.AggregateModel.AgencyAggregate
{
    public class Agency
    {
        public Agency(int agencyId, string agencyPin, string agencyName, string location, int virtualConcurrentUser, int physicalConcurrentUser)
        {
            AgencyId = agencyId;
            AgencyPin = agencyPin;
            AgencyName = agencyName;
            Location = location;
            VirtualConcurrentUser = virtualConcurrentUser;
            PhysicalConcurrentUser = physicalConcurrentUser;
        }

        public int AgencyId { get; private set; }
        public string AgencyPin { get; private set; }
        public string AgencyName { get; private set; }
        public string Location { get; private set; }
        public int VirtualConcurrentUser { get; private set; }
        public int PhysicalConcurrentUser { get; private set; }
    }
}
