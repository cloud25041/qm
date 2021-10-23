using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meeting_Application.IntegrationEvents.OutgoingEvents
{
    public record ZoomLinkCreatedIntegrationEvent(
        Guid AppointmentId,
        string ZoomLink) : IntegrationEvent;
}
