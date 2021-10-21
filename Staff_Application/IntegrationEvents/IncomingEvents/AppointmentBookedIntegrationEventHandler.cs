using EventBus.Abstractions;
using MediatR;
using Staff_Domain.AggregateModel.AppointmentAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Staff_Application.IntegrationEvents.IncomingEvents
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
            
            Appointment appointment = new Appointment(request.AppointmentId, request.AgencyId, request.AppointmentDate, request.AppointmentSlotId, request.CustomerAccountId, request.AppointmentState, request.CustomerName);
            _appointmentRepository.Add(appointment);
            await _appointmentRepository.UnitOfWork.SaveEntitiesAsync();
            
            return Unit.Value;
        }
    }
}
