using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Staff_Application.IntegrationEvents.Events
{
    public record AppointmentBookedIntegrationEvent : IntegrationEvent, IRequest
    {
        public AppointmentBookedIntegrationEvent(Guid appointmentId, int agencyId, DateTime appointmentDate, int appointmentSlotId, Guid userAccountId, int appointmentState)
        {
            AppointmentId = appointmentId;
            AgencyId = agencyId;
            AppointmentDate = appointmentDate;
            AppointmentSlotId = appointmentSlotId;
            UserAccountId = userAccountId;
            AppointmentState = appointmentState;
        }

        public Guid AppointmentId { get; init; }
        public int AgencyId { get; init; }
        public DateTime AppointmentDate { get; init; }
        public int AppointmentSlotId { get; init; }
        public Guid UserAccountId { get; init; }
        public int AppointmentState { get; init; }
    }
}
