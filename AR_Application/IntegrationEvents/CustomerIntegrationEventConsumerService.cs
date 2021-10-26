using EventBus.MQTT;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AR_Application.IntegrationEvents
{
    public class CustomerIntegrationEventConsumerService : IHostedService
    {
        private readonly IMQClient _mQClient;
        public IServiceProvider Service { get; private set; }

        public CustomerIntegrationEventConsumerService(IMQClient mQClient, IServiceProvider service)
        {
            _mQClient = mQClient;
            Service = service;
            _mQClient.MqttTopicUpdateEvent += EventMessageHandler;
        }

        private async void EventMessageHandler(object sender, MqttTopicUpdateEventArg e)
        {
            Type eventType = GetEventTypeByName(e.Topic.TopicName);
            object integrationEvent = JsonConvert.DeserializeObject(e.Topic.Value, eventType);
            // trigger integration event command

            using (var scope = Service.CreateScope())
            {
                IMediator _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await _mediator.Send(integrationEvent);
            }
        }

        private Type GetEventTypeByName(string eventName)
        {
            Type eventType;
            _mQClient.DictOfSubscribedTopics.TryGetValue(eventName, out eventType);
            return eventType;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        
    }
}
