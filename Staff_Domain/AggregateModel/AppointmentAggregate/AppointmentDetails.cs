using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Domain.AggregateModel.AppointmentAggregate
{
    public class AppointmentDetails
    {
        public Guid AccountId { get; private set; }
        public string AccountName { get; private set; }

        public AppointmentDetails(Guid accountId, string accountName)
        {
            AccountId = accountId;
            AccountName = accountName;
        }
    }
}
