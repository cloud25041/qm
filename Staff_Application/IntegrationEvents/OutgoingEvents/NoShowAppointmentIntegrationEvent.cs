using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Application.IntegrationEvents.OutgoingEvents
{
    public record AppointmentCompletedIntegrationEvent(
        Guid AppointmentId) : IntegrationEvent;
}
