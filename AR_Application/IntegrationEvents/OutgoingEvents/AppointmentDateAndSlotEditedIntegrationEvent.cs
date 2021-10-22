using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AR_Application.IntegrationEvents.OutgoingEvents
{
    public record AppointmentDateAndSlotEditedIntegrationEvent(
        Guid AppointmentId,
        DateTime Date,
        int Slot) : IntegrationEvent;
}
