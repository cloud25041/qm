using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AR_Domain.AggregateModel.AppointmentAggregate
{
    public class Appointment
    {
        public Guid AppointmentId { get; private set; }
        public Guid AccountId { get; private set; }
        public int AppointmentState { get; private set; }
        public string AgencyCode { get; private set; }
        public DateTime DateTime { get; private set; }

        public Appointment(Guid accountId, string agencyCode, DateTime dt)
        {
            AppointmentId = new Guid();
            AccountId = accountId;
            AppointmentState = 0;
            AgencyCode = agencyCode;
            DateTime = dt;
        }

        public Appointment CreateAppointment()
        {
            return this;
        }

        public Appointment UpdateState(int state)
        {
            // update state
            return this;
        }
    }
}
