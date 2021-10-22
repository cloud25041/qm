using EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using AR_Domain.AggregateModel.AppointmentAggregate;

namespace AR_Application.IntegrationEvents.IncomingEvents
{
    public class AppointmentConfirmedByStaffIntegrationEventHandler : IIntegrationEventHandler<AppointmentConfirmedByStaffIntegrationEvent>, IRequestHandler<AppointmentConfirmedByStaffIntegrationEvent>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentConfirmedByStaffIntegrationEventHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Unit> Handle(AppointmentConfirmedByStaffIntegrationEvent request, CancellationToken cancellationToken)
        {
            // Eric - Retreive appointment and make modification to entity.
            Appointment appointment = await _appointmentRepository.GetAppointmentByIdAsync(request.AppointmentId);
            appointment.SetStaffIdOnceStaffConfirmedThisAppointment(request.StaffId);

            // Eric - Update entity in repository.
            _appointmentRepository.Update(appointment);
            await _appointmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
