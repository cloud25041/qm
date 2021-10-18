using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AR_Domain.AggregateModel.AccountAggregate;
using AR_Domain.AggregateModel.AppointmentAggregate;
using AR_Application.IntegrationEvents;
using AR_Application.IntegrationEvents.Events;

namespace AR_Application.Commands
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, bool>
    {
        public CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IAccountRepository accountRepository, ICustomerIntegrationEventService customerIntegrationEventService)
        {
            _appointmentRepository = appointmentRepository;
            _accountRepository = accountRepository;
            _customerIntegrationEventService = customerIntegrationEventService;
        }

        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerIntegrationEventService _customerIntegrationEventService;

        public async Task<bool> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = new Appointment(request.agencyId, request.appointmentType, request.appointmentState, request.appointmentDate, request.appointmentSlotId, request.userAccountId);

            AppointmentBookedIntegrationEvent appointmentBookedIntegrationEvent = new AppointmentBookedIntegrationEvent(appointment.AppointmentId, appointment.AgencyId, appointment.AppointmentDate, appointment.AppointmentSlotId, appointment.UserAccountId, appointment.AppointmentState);
            await _customerIntegrationEventService.AddAndSaveEventAsync(appointmentBookedIntegrationEvent);

            _appointmentRepository.Add(appointment);
            return await _appointmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
       
    }
}
