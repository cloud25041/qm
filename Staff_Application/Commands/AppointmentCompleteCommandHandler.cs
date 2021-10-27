using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Staff_Domain.AggregateModel;
using MediatR;
using Staff_Domain.AggregateModel.AccountAggregate;
using System.Threading;
using Staff_Domain.AggregateModel.AppointmentAggregate;
using Staff_Application.IntegrationEvents;
using Staff_Application.IntegrationEvents.OutgoingEvents;

namespace Staff_Application.Commands
{
    public class AppointmentCompleteCommandHandler : IRequestHandler<AppointmentCompleteCommand, bool>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IStaffIntegrationEventService _staffIntegrationEventService;
        public AppointmentCompleteCommandHandler(IAppointmentRepository appointmentRepository, IStaffIntegrationEventService staffIntegrationEventService)
        {
            _appointmentRepository = appointmentRepository;
            _staffIntegrationEventService = staffIntegrationEventService;
        }

        public async Task<bool> Handle(AppointmentCompleteCommand request, CancellationToken cancellationToken)
        {
            Appointment appointment = await _appointmentRepository.GetAppointmentByIdAsync(request.AppointmentId);
            if(appointment == null)
            {
                throw new Exception("appointment cannot be found with this Appointment ID: " + request.AppointmentId.ToString());
            }
            appointment.CompleteAppointment();
            _appointmentRepository.Update(appointment);
            await _appointmentRepository.UnitOfWork.SaveEntitiesAsync();

            await _staffIntegrationEventService.AddAndSaveEventAsync(new AppointmentCompletedIntegrationEvent(request.AppointmentId));
            return true;
        }
    }

}
