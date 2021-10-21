using AR_Application.IntegrationEvents;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System;
using EventBus.Extensions;

namespace AR_Application.Behaviours
{

    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICustomerIntegrationEventService _customerIntegrationEventService;
        public readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        public TransactionBehaviour(ICustomerIntegrationEventService customerIntegrationEventService, ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _customerIntegrationEventService = customerIntegrationEventService;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            try
            {
                _logger.LogInformation("{datetime} Begin transaction for {commandName} ({@command})", DateTime.Now.ToString(), request.GetGenericTypeName(), request);
                response = await next();
                await _customerIntegrationEventService.PublishEventsThroughEventBusAsync();
                _logger.LogInformation("{datetime} Completed transaction for {commandName} ({@command})", DateTime.Now.ToString(), request.GetGenericTypeName(), request);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "{datetime} ERROR Handling transaction for {CommandName} ({@Command})", DateTime.Now.ToString(), request.GetGenericTypeName(), request);
            }
            return response;
        }
    }
}
