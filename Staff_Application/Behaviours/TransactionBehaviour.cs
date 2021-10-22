using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventBus.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Staff_Application.IntegrationEvents;

namespace Staff_Application.Behaviours
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IStaffIntegrationEventService _staffIntegrationEventService;
        public readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        public TransactionBehaviour(IStaffIntegrationEventService staffIntegrationEventService, ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _staffIntegrationEventService = staffIntegrationEventService;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            try
            {
                _logger.LogInformation("{datetime} Begin transaction for {commandName} ({@command})", DateTime.Now.ToString(), request.GetGenericTypeName(), request);
                response = await next();
                await _staffIntegrationEventService.PublishEventsThroughEventBusAsync();
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
