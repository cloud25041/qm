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

namespace Meeting_Application.IntegrationEvents
{
    public class MeetingIntegrationEventConsumerService : IHostedService
    {

        private readonly IMQClient _mQClient;
        public IServiceProvider ServiceProvider { get; private set; }
        public MeetingIntegrationEventConsumerService(IMQClient mQClient, IServiceProvider serviceProvider)
        {
            _mQClient = mQClient;
            ServiceProvider = serviceProvider;
            _mQClient.MqttTopicUpdateEvent += EventMessageHandler;
        }

        private async void EventMessageHandler(object sender, MqttTopicUpdateEventArg e)
        {
            Type eventType = GetEventTypeByName(e.Topic.TopicName);
            object integrationEvent = JsonConvert.DeserializeObject(e.Topic.Value, eventType);
            // trigger integration event command

            using (var scope = ServiceProvider.CreateScope())
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
