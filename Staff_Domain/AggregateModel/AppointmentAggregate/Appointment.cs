using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Staff_Domain.DomainEvents;
using Staff_Domain.SeedWork;

namespace Staff_Domain.AggregateModel.AppointmentAggregate
{
    public class Appointment : Entity, IAggregateRoot
    {
        public Guid AppointmentId { get; private set; }
        public Guid AccountId { get; private set; }
        public int AppointmentState { get; private set; }
        public string AgencyCode { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        Appointment()
        {

        }

        public Appointment(string username, string agencyCode, DateTime startTime, DateTime endTime)
        {
            AppointmentId = new Guid();
            AppointmentState = 0;
            AgencyCode = agencyCode;
            StartTime = startTime;
            EndTime = endTime;

        }

        public Appointment(Guid appointmentId, Guid accountId, int appointmentState, string agencyCode, DateTime startTime, DateTime endTime)
        {
            AppointmentId = appointmentId;
            AccountId = accountId;
            AppointmentState = appointmentState;
            AgencyCode = agencyCode;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Appointment SetAccountIdOnceUsernameIsVerified(Guid accountId)
        {
            AccountId = accountId;
            return this;
        }

        public Appointment CreateAppointment()
        {
            throw new NotImplementedException();
        }

        public Appointment ChangeState(int state)
        {
            // update state
            throw new NotImplementedException();
        }
    }
}
