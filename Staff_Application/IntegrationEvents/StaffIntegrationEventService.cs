using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.MQTT;
using Newtonsoft.Json;


namespace Staff_Application.IntegrationEvents
{
    public class StaffIntegrationEventService : IStaffIntegrationEventService
    {

        private List<IntegrationEvent> _integrationEvents;
        private readonly IMQClient _mQClient;

        public StaffIntegrationEventService(IMQClient mQClient)
        {
            _mQClient = mQClient;
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            await Task.Run(() =>
            {
                if (_integrationEvents == null)
                    _integrationEvents = new();

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
