using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Application.IntegrationEvents.OutgoingEvents
{
    public record AppointmentConfirmedByStaffIntegrationEvent(
        Guid AppointmentId,
        Guid StaffId) : IntegrationEvent;
}
