using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Staff_Application.IntegrationEvents.IncomingEvents
{
    public record AppointmentBookedIntegrationEvent : IntegrationEvent, IRequest
    {
        public AppointmentBookedIntegrationEvent(Guid appointmentId, int agencyId, DateTime appointmentDate, int appointmentSlotId, Guid customerAccountId, int appointmentState, string customerName)
        {
            AppointmentId = appointmentId;
            AgencyId = agencyId;
            AppointmentDate = appointmentDate;
            AppointmentSlotId = appointmentSlotId;
            CustomerAccountId = customerAccountId;
            AppointmentState = appointmentState;
            CustomerName = customerName;
        }

        public Guid AppointmentId { get; init; }
        public int AgencyId { get; init; }
        public DateTime AppointmentDate { get; init; }
        public int AppointmentSlotId { get; init; }
        public Guid CustomerAccountId { get; init; }
        public string CustomerName { get; init; }
        public int AppointmentState { get; init; }
    }
}
