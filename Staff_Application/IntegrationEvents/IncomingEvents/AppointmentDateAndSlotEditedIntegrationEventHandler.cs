using EventBus.Abstractions;
using MediatR;
using Staff_Domain.AggregateModel.AppointmentAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Staff_Application.IntegrationEvents.IncomingEvents
{
    public class AppointmentDateAndSlotEditedIntegrationEventHandler : IIntegrationEventHandler<AppointmentDateAndSlotEditedIntegrationEvent>, IRequestHandler<AppointmentDateAndSlotEditedIntegrationEvent>
    {
        public readonly IAppointmentRepository _appointmentRepository;
        public AppointmentDateAndSlotEditedIntegrationEventHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Unit> Handle(AppointmentDateAndSlotEditedIntegrationEvent request, CancellationToken cancellationToken)
        {
            Appointment appointment = await _appointmentRepository.GetAppointmentByIdAsync(request.AppointmentId);
            if (appointment == null)
                throw new Exception("Appointmene does not exist with this appointment id:" + request.AppointmentId);

            appointment.EditAppointmentDateAndSlot(request.Date, request.Slot);
            _appointmentRepository.Update(appointment);
            await _appointmentRepository.UnitOfWork.SaveEntitiesAsync();
            return Unit.Value;
        }
    }
}
