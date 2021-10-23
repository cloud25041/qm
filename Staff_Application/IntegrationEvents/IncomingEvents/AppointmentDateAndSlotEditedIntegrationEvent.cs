using EventBus.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Application.IntegrationEvents.IncomingEvents
{
    public record AppointmentDateAndSlotEditedIntegrationEvent(
        Guid AppointmentId,
        DateTime Date,
        int Slot) : IntegrationEvent, IRequest;
}
