using EventBus.Abstractions;
using EventBus.Events;
using System;
using System.Collections.Generic;

namespace EventBus.MQTT
{
    public interface IMQClient
    {
        Dictionary<string, Type> DictOfSubscribedTopics { get; set; }
        bool IsConnected { get; }
        string MqttBroker { get; set; }
        string MqttPassword { get; set; }
        int MqttPort { get; set; }
        string MqttUserId { get; set; }
        bool UsingLocalBroker { get; set; }

        event EventHandler<MqttTopicUpdateEventArg> MqttTopicUpdateEvent;

        void Connect();
        void Disconnect();
        void Dispose();
        void PublishUpdate(IntegrationEvent integrationEvent);
        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
        void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
    }
}