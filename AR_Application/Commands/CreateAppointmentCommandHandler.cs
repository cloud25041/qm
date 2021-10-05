using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AR_Domain.SeedWork;
using AR_Domain.AggregateModel.AccountAggregate;
using AR_Domain.AggregateModel.AppointmentAggregate;

namespace AR_Application.Commands
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, bool>
    {
        public CreateAppointmentCommandHandler(IAccountRepository accountRepository, IAppointmentRepository appointmentRepository)
        {
            _accountRepository = accountRepository;
            _appointmentRepository = appointmentRepository;
        }

        private readonly IAccountRepository _accountRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public async Task<bool> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            Account account = await _accountRepository.GetAsync(request.username);
            // validate appointment
            if (account == null)
            {
                return false;
            }

            throw new NotImplementedException();
        }
    }
}
