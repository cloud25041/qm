using System;

namespace EventBus.MQTT
{
    public class MqttTopicUpdateEventArg : EventArgs
    {
        public MQTopic Topic { get; set; }

        public MqttTopicUpdateEventArg(MQTopic topic)
        {
            Topic = topic;
        }
    }
}
