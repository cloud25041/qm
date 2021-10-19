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
    public class AssignStaffToAppointmentCommandHandler : IRequestHandler<AssignStaffToAppointmentCommand, bool>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public AssignStaffToAppointmentCommandHandler(IAccountRepository accountRepository, IAppointmentRepository appointmentRepository)
        {
            _accountRepository = accountRepository;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<bool> Handle(AssignStaffToAppointmentCommand request, CancellationToken cancellationToken)
        {
            Account account = await _accountRepository.GetAccountByIdAsync(request.StaffId);
            if (account == null)
                return false;

            Appointment appointment = await _appointmentRepository.GetAppointmentByIdAsync(request.AppointmentId);
            appointment.SetStaffAccountIdOnceStaffConfirmAppointment(account.AccountId);
            _appointmentRepository.Update(appointment);
            await _appointmentRepository.UnitOfWork.SaveEntitiesAsync();
            return true;
        }
    }
}
