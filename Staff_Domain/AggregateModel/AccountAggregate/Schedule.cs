using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Domain.AggregateModel.AccountAggregate
{
    public class Schedule
    {
        public List<bool> AvailableSlots { get; private set; }

        public Schedule()
        {
            if(AvailableSlots == null)
            {
                AvailableSlots = new();
            }
            for (int i=1; i <= 24; i++)
            {
                AvailableSlots.Add(true);
            }
        }
    }
}
