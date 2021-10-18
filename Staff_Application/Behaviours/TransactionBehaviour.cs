using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Staff_Application.IntegrationEvents;

namespace Staff_Application.Behaviours
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IStaffIntegrationEventService _staffIntegrationEventService;

        public TransactionBehaviour(IStaffIntegrationEventService staffIntegrationEventService)
        {
            _staffIntegrationEventService = staffIntegrationEventService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();
            //await _staffIntegrationEventService.PublishEventsThroughEventBusAsync();
            return response;
        }
    }
}
