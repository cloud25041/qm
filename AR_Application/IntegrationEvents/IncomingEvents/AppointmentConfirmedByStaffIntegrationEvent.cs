using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace AR_Application.IntegrationEvents.IncomingEvents
{
    public record AppointmentConfirmedByStaffIntegrationEvent(
        Guid AppointmentId,
        Guid StaffId) : IntegrationEvent, IRequest;
}
