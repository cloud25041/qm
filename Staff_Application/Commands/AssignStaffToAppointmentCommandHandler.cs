using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Staff_Application.IntegrationEvents;
using Staff_Application.IntegrationEvents.OutgoingEvents;
using Staff_Domain.AggregateModel.AccountAggregate;
using Staff_Domain.AggregateModel.AppointmentAggregate;

namespace Staff_Application.Commands
{
    public class AssignStaffToAppointmentCommandHandler : IRequestHandler<AssignStaffToAppointmentCommand, bool>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IStaffIntegrationEventService _staffIntegrationEventService;

        public AssignStaffToAppointmentCommandHandler(IAccountRepository accountRepository, IAppointmentRepository appointmentRepository, IStaffIntegrationEventService staffIntegrationEventService)
        {
            _accountRepository = accountRepository;
            _appointmentRepository = appointmentRepository;
            _staffIntegrationEventService = staffIntegrationEventService;
        }

        public async Task<bool> Handle(AssignStaffToAppointmentCommand request, CancellationToken cancellationToken)
        {
            // Eric - Check account valid
            Account account = await _accountRepository.GetAccountByIdAsync(request.StaffId);
            if (account == null)
                return false;

            // Eric - Get appointment and make modification
            Appointment appointment = await _appointmentRepository.GetAppointmentByIdAsync(request.AppointmentId);
            appointment.SetStaffAccountIdOnceStaffConfirmAppointment(account.AccountId);
            _appointmentRepository.Update(appointment);

            // Eric - Populate new integration event
            AppointmentConfirmedByStaffIntegrationEvent integrationEvent = new AppointmentConfirmedByStaffIntegrationEvent(appointment.AppointmentId, appointment.StaffAccountId, appointment.AppointmentState);
            await _staffIntegrationEventService.AddAndSaveEventAsync(integrationEvent);

            await _appointmentRepository.UnitOfWork.SaveEntitiesAsync();
            return true;
        }
    }
}
