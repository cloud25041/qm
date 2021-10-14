using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Staff_Domain.AggregateModel.AccountAggregate;
using Staff_Domain.AggregateModel.AppointmentAggregate;

namespace Staff_Application.Commands
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, bool>
    {
        public CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IAccountRepository accountRepository)
        {
            _appointmentRepository = appointmentRepository;
            _accountRepository = accountRepository;
        }

        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAccountRepository _accountRepository;

        public async Task<bool> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            var account = await _accountRepository.GetAsync(request.username);
            if (account == null)
                return false;

            var appointment = new Appointment(request.username, request.agencyCode, request.startTime, request.endTime);
            appointment.SetAccountIdOnceUsernameIsVerified(account.AccountId);
            _appointmentRepository.Add(appointment);
            return await _appointmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
