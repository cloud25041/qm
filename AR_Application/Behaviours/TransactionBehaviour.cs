using AR_Application.IntegrationEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AR_Application.Behaviours
{

    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICustomerIntegrationEventService _customerIntegrationEventService;
        public TransactionBehaviour(ICustomerIntegrationEventService customerIntegrationEventService)
        {
            _customerIntegrationEventService = customerIntegrationEventService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();

            await _customerIntegrationEventService.PublishEventsThroughEventBusAsync();
            return response;
        }
    }
}
