using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Staff_Application.Commands
{
    public record AssignStaffToAppointmentCommand : IRequest<bool>
    {
        public Guid StaffId { get; init; }
        public Guid AppointmentId { get; init; }
    }
}
