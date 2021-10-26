using AR_Domain.AggregateModel.AppointmentAggregate;
using EventBus.Abstractions;
using EventBus.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AR_Application.IntegrationEvents.IncomingEvents
{
    public class NoShowAppointmentIntegrationEventHandler : IIntegrationEventHandler<NoShowAppointmentIntegrationEvent>, IRequestHandler<NoShowAppointmentIntegrationEvent>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public NoShowAppointmentIntegrationEventHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;

        }

        public async Task<Unit> Handle(NoShowAppointmentIntegrationEvent request, CancellationToken cancellationToken)
        {
            Appointment appointment = await _appointmentRepository.GetAppointmentByIdAsync(request.AppointmentId);
            if (appointment == null)
            {
                throw new Exception("appointment cannot be found with this Appointment ID: " + request.AppointmentId.ToString());
            }

            appointment.NoShowAppointment();
            _appointmentRepository.Update(appointment);
            await _appointmentRepository.UnitOfWork.SaveEntitiesAsync();
            return Unit.Value;
        }
    }
}
