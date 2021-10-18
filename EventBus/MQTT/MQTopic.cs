using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.MQTT
{
    [Serializable]
    public class MQTopic
    {
        public string TopicName { get; set; }
        public string Value { get; set; }

        public MQTopic()
        {
        }

        public MQTopic(string topicName, string value)
        {
            if (string.IsNullOrWhiteSpace(topicName))
                throw new InvalidOperationException("Topic must be specified");

            TopicName = topicName;
            Value = value;
        }
    }
}
