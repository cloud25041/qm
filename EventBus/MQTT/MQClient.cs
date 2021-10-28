using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventBus.Abstractions;
using EventBus.Events;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;

namespace EventBus.MQTT
{
    public class MQClient : IDisposable, IMQClient
    {
        private static int DefaultMqttPort = 1883;

        private MqttFactory _MqttFactory = null;
        private IManagedMqttClient _MqttClient = null;

        // -----------------------------------------------------------

        #region Public Properties

        public string MqttBroker { get; set; }
        public int MqttPort { get; set; }
        public string MqttUserId { get; set; }
        public string MqttPassword { get; set; }
        public bool UsingLocalBroker { get; set; }

        public bool IsConnected
        {
            get
            {
                return (_MqttClient != null) ? _MqttClient.IsConnected : false;
            }
        }

        public Dictionary<string, Type> DictOfSubscribedTopics { get; set; }

        #endregion

        // -----------------------------------------------------------



        #region Public Methods

        public async void Connect()
        {
            if (_MqttClient == null)
                throw new InvalidOperationException("MQTT client service not available");

            try
            {
                // Setup and start a managed MQTT client.
                ManagedMqttClientOptions options;
                if (UsingLocalBroker == false)
                {
                    options = new ManagedMqttClientOptionsBuilder()
                    .WithAutoReconnectDelay(TimeSpan.FromSeconds(10))
                    .WithClientOptions(new MqttClientOptionsBuilder()
                        .WithClientId(Guid.NewGuid().ToString())
                        .WithCleanSession(true)
                        .WithCredentials(MqttUserId, MqttPassword)
                        .WithTcpServer(MqttBroker, MqttPort)
                        .WithTls(new MqttClientOptionsBuilderTlsParameters()
                        {
                            UseTls = true,
                            AllowUntrustedCertificates = true,
                            SslProtocol = System.Security.Authentication.SslProtocols.Tls12
                        })
                        .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V500)
                        .Build())
                    .Build();
                }
                else
                {
                    options = new ManagedMqttClientOptionsBuilder()
                    .WithAutoReconnectDelay(TimeSpan.FromSeconds(10))
                    .WithClientOptions(new MqttClientOptionsBuilder()
                        .WithClientId(Guid.NewGuid().ToString())
                        .WithCleanSession(true)
                        .WithCredentials(MqttUserId, MqttPassword)
                        .WithTcpServer(MqttBroker, MqttPort)
                        .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V311)
                        .Build())
                    .Build();
                }

                _MqttClient.ApplicationMessageReceivedHandler =
                  new MqttApplicationMessageReceivedHandlerDelegate(ex =>
                  {
                      OnMessageReceived(ex);
                  });

                await _MqttClient.StartAsync(options);

                _MqttClient.ConnectingFailedHandler =
                  new ConnectingFailedHandlerDelegate(ex =>
                  {
                  });
            }
            catch
            {
                throw;
            }
        }

        public async void Disconnect()
        {
            try
            {
                if (_MqttClient != null && _MqttClient.IsConnected)
                {
                    _MqttClient.ApplicationMessageReceivedHandler = null;
                    await _MqttClient.StopAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            string topic = typeof(T).Name;
            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentNullException();
            if (_MqttClient == null)
                throw new InvalidOperationException("MQTT client service not available");

            try
            {
                await _MqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
                    .WithTopic(topic)
                    .WithAtLeastOnceQoS()
                    .Build());

                if (DictOfSubscribedTopics == null)
                    DictOfSubscribedTopics = new();
                DictOfSubscribedTopics.Add(topic, typeof(T));
            }
            catch
            {
                throw;
            }
        }

        public async void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            string topic = typeof(T).Name;
            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentNullException();
            if (_MqttClient == null)
                throw new InvalidOperationException("MQTT client service not available");

            try
            {
                await _MqttClient.UnsubscribeAsync(new string[] { topic });
            }
            catch
            {
                throw;
            }
        }

        public async void PublishUpdate(IntegrationEvent integrationEvent)
        {
            string topic = integrationEvent.GetType().Name;
            string payload = JsonConvert.SerializeObject(integrationEvent);

            if (string.IsNullOrWhiteSpace(topic) || string.IsNullOrEmpty(payload))
                throw new ArgumentNullException();
            if (_MqttClient == null)
                throw new InvalidOperationException("MQTT client service not available");

            try
            {
                var message = new MqttApplicationMessageBuilder()
                  .WithTopic(topic)
                  .WithPayload(payload)
                  .WithAtLeastOnceQoS()
                  .WithRetainFlag(false)
                  .Build();

                await _MqttClient.PublishAsync(message);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        // -----------------------------------------------------------

        #region Public Events

        public event EventHandler<MqttTopicUpdateEventArg> MqttTopicUpdateEvent;

        #endregion

        // -----------------------------------------------------------

        public MQClient()
        {
            try
            {
                _MqttFactory = new MqttFactory();
                _MqttClient = _MqttFactory.CreateManagedMqttClient();

                MqttPort = DefaultMqttPort;
            }
            catch
            {
                throw;
            }
        }

        ~MQClient()
        {
            Dispose(false);
        }

        //Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_MqttClient != null)
                {
                    _MqttClient.Dispose();
                    _MqttClient = null;
                }
            }
            // Free your own state (unmanaged objects).
            // Set large fields to null.
        }

        private void OnMessageReceived(MqttApplicationMessageReceivedEventArgs ex)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ex.ApplicationMessage.Topic))
                {
                    OnMqttTopicUpdateEvent(new MqttTopicUpdateEventArg(
                      new MQTopic(ex.ApplicationMessage.Topic, ex.ApplicationMessage.ConvertPayloadToString())));
                }
            }
            catch (Exception exx)
            {
                Console.WriteLine(exx.ToString());
            }
        }

        protected void OnMqttTopicUpdateEvent(MqttTopicUpdateEventArg e)
        {
            EventHandler<MqttTopicUpdateEventArg> handler = MqttTopicUpdateEvent;
            if (handler != null)
                handler(this, e);
        }

    }
}
