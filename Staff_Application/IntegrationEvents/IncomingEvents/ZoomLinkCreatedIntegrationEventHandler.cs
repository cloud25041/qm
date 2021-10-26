using Staff_Domain.AggregateModel.AppointmentAggregate;
using EventBus.Abstractions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Staff_Application.IntegrationEvents.IncomingEvents
{
    public class ZoomLinkCreatedIntegrationEventHandler : IIntegrationEventHandler<ZoomLinkCreatedIntegrationEvent>, IRequestHandler<ZoomLinkCreatedIntegrationEvent>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public ZoomLinkCreatedIntegrationEventHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<Unit> Handle(ZoomLinkCreatedIntegrationEvent request, CancellationToken cancellationToken)
        {
            // Eric - Retreive appointment and make modification to entity.
            Appointment appointment = await _appointmentRepository.GetAppointmentByIdAsync(request.AppointmentId);
            appointment.SetZoomLink(request.ZoomLink);

            // Eric - Update entity in repository.
            _appointmentRepository.Update(appointment);
            await _appointmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
