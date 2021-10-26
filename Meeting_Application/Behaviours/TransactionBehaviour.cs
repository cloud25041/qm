using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventBus.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Meeting_Application.IntegrationEvents;

namespace Meeting_Application.Behaviours
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IMeetingIntegrationEventService _meetingIntegrationEventService;
        public readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        public TransactionBehaviour(IMeetingIntegrationEventService meetingIntegrationEventService, ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _meetingIntegrationEventService = meetingIntegrationEventService;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            try
            {
                _logger.LogInformation("{datetime} Begin transaction for {commandName} ({@command})", DateTime.Now.ToString(), request.GetGenericTypeName(), request);
                response = await next();
                await _meetingIntegrationEventService.PublishEventsThroughEventBusAsync();
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
