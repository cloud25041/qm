using AR_Application.IntegrationEvents;
using AR_Application.IntegrationEvents.OutgoingEvents;
using AR_Domain.AggregateModel.AccountAggregate;
using AR_Domain.AggregateModel.AppointmentAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
            Appointment appointment = new Appointment(request.agencyId, request.appointmentType, request.appointmentState, request.appointmentDate, request.appointmentSlotId, request.userAccountId);

            Account account = await _accountRepository.GetAccountByIdAsync(appointment.UserAccountId);
            if (account == null)
                throw new System.Exception("Account not found using this account Id :" + appointment.UserAccountId.ToString());

            AppointmentBookedIntegrationEvent appointmentBookedIntegrationEvent = new AppointmentBookedIntegrationEvent(
                appointment.AppointmentId, 
                appointment.AgencyId, 
                appointment.AppointmentDate, 
                appointment.AppointmentSlotId, 
                appointment.UserAccountId, 
                appointment.AppointmentState,
                account.AccountDetails.Name);
            await _customerIntegrationEventService.AddAndSaveEventAsync(appointmentBookedIntegrationEvent);

            _appointmentRepository.Add(appointment);
            return await _appointmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
       
    }
}
