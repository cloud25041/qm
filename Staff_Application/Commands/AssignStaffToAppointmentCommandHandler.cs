using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Staff_Application.Commands
{
    public class AssignStaffToAppointmentCommandHandler : IRequestHandler<AssignStaffToAppointmentCommand, bool>
    {
        public AssignStaffToAppointmentCommandHandler()
        {
        }

        public Task<bool> Handle(AssignStaffToAppointmentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
