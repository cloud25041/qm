using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Staff_Domain.AggregateModel;
using MediatR;
using Staff_Domain.AggregateModel.AccountAggregate;

namespace Staff_Application.Commands
{
    public record AppointmentCompleteCommand(Guid AppointmentId) : IRequest<bool>;
}
