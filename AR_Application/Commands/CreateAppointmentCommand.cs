using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace AR_Application.Commands
{
    public record CreateAppointmentCommand(
        int agencyId,
        int appointmentType,
        int appointmentState,
        DateTime appointmentDate,
        int appointmentSlotId,
        Guid userAccountId) : IRequest<bool>;

}
