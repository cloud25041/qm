using EventBus.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AR_Application.IntegrationEvents.IncomingEvents
{
    public record AppointmentCompletedIntegrationEvent(
        Guid AppointmentId) : IntegrationEvent, IRequest;
}
