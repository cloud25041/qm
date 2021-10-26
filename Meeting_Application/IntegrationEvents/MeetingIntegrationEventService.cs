using EventBus.Events;
using EventBus.MQTT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meeting_Application.IntegrationEvents
{
    public class MeetingIntegrationEventService : IMeetingIntegrationEventService
    {
        private List<IntegrationEvent> _integrationEvents;
        private readonly IMQClient _mQClient;

        public MeetingIntegrationEventService(IMQClient mQClient)
        {
            _mQClient = mQClient;
            _integrationEvents = new();
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            await Task.Run(() =>
            {
                _integrationEvents.Add(evt);
            });
        }

        public async Task PublishEventsThroughEventBusAsync()
        {
            await Task.Run(() =>
            {
                foreach (IntegrationEvent integrationEvent in _integrationEvents)
                {
                    _mQClient.PublishUpdate(integrationEvent);
                }
                _integrationEvents = new();
            });
        }
    }
}
