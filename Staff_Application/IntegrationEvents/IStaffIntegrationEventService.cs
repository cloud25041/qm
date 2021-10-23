using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Staff_Application.IntegrationEvents
{
    public interface IStaffIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync();
        Task AddAndSaveEventAsync(IntegrationEvent evt);
        void AddAndSaveEventAsync();
    }
}
