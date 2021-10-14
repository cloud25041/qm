using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Staff_Application.Commands
{
    public record CreateAppointmentCommand(
        string username,
        string agencyCode,
        DateTime startTime,
        DateTime endTime) : IRequest<bool>;

}
