using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace AR_Application.Commands
{
    public record EditAppointmentDateAndSlotCommand(
        Guid AppointmentId,
        DateTime Date,
        int Slot) : IRequest<bool>;
}
