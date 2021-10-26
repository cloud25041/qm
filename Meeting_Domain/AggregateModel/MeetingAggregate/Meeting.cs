using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meeting_Domain.AggregateModel.MeetingAggregate
{
    public class Meeting
    {
        public Guid MeetingId { get; private set; }
        public Guid AccountId { get; private set; }
    }
}
