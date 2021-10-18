using EventBus.Abstractions;
using Staff_Application.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using Staff_Domain.AggregateModel.AppointmentAggregate;

namespace Staff_Application.IntegrationEvents.EventHandling
{
    public class AppointmentBookedIntegrationEventHandler : IIntegrationEventHandler<AppointmentBookedIntegrationEvent>, IRequestHandler<AppointmentBookedIntegrationEvent>
    {
        
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentBookedIntegrationEventHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        
        public async Task<Unit> Handle(AppointmentBookedIntegrationEvent request, CancellationToken cancellationToken)
        {
            
            Appointment appointment = new Appointment(request.AppointmentId, request.AgencyId, request.AppointmentDate, request.AppointmentSlotId, request.UserAccountId, request.AppointmentState);
            _appointmentRepository.Add(appointment);
            await _appointmentRepository.UnitOfWork.SaveEntitiesAsync();
            
            return Unit.Value;
        }
    }
}
