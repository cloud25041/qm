using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AR_Domain.SeedWork;

namespace AR_Domain.AggregateModel.AppointmentAggregate
{
    public class Appointment : IAggregateRoot
    {
        public Guid AppointmentId { get; private set; }
        public Guid AccountId { get; private set; }
        public int AppointmentState { get; private set; }
        public string AgencyCode { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public Appointment(Guid accountId, string agencyCode, DateTime startTime, DateTime endTime)
        {
            AppointmentId = new Guid();
            AccountId = accountId;
            AppointmentState = 0;
            AgencyCode = agencyCode;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Appointment CreateAppointment()
        {
            return this;
        }

        public Appointment ChangeState(int state)
        {
            // update state
            return this;
        }
    }
}
