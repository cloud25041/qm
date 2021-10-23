using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AR_Application.IntegrationEvents;
using AR_Application.IntegrationEvents.OutgoingEvents;
using AR_Domain.AggregateModel.AppointmentAggregate;
using MediatR;

namespace AR_Application.Commands
{
    public class EditAppointmentDateAndSlotCommandHandler : IRequestHandler<EditAppointmentDateAndSlotCommand, bool>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ICustomerIntegrationEventService _customerIntegrationEventService;
        public EditAppointmentDateAndSlotCommandHandler(IAppointmentRepository appointmentRepository, ICustomerIntegrationEventService customerIntegrationEventService)
        {
            _appointmentRepository = appointmentRepository;
            _customerIntegrationEventService = customerIntegrationEventService;
        }

        public async Task<bool> Handle(EditAppointmentDateAndSlotCommand request, CancellationToken cancellationToken)
        {
            Appointment appointment = await _appointmentRepository.GetAppointmentByIdAsync(request.AppointmentId);
            if (appointment == null)
                throw new Exception("Appointment does not exist with this appointment id:" + request.AppointmentId);

            appointment.EditAppointmentDateAndSlot(request.Date, request.Slot);
            _appointmentRepository.Update(appointment);
            await _appointmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            AppointmentDateAndSlotEditedIntegrationEvent appointmentDateAndSlotEditedIntegrationEvent = new AppointmentDateAndSlotEditedIntegrationEvent(
                appointment.AppointmentId,
                appointment.AppointmentDate,
                appointment.AppointmentSlotId);
            await _customerIntegrationEventService.AddAndSaveEventAsync(appointmentDateAndSlotEditedIntegrationEvent);
            return true;
        }
    }
}
