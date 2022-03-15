using MQTTnet;
using MQTTnet.Client;

namespace DotnetRcCar.Common
{
    public class MqttMessageListener : IMessageListener
    {
        public event DeviceMessageReceived DeviceMessageReceived;


        private readonly IMqttClient _mqttClient;
        private bool _started;

        public MqttMessageListener(IMqttClient mqttClient)
        {
            _mqttClient = mqttClient;
          
          
        }

        public async Task Start()
        {
            if (_started) return;
            _mqttClient.UseApplicationMessageReceivedHandler(HandleMessage);
            var mqttFactory = new MqttFactory();
            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic(MqttTopics.StatesTopic); })
                .WithTopicFilter(f=> { f.WithTopic(MqttTopics.CommandsTopic); })
                .Build();
            await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
            _started = true;
        }

        public async Task Stop()
        {
            if( !_started) return;
           
            var mqttFactory = new MqttFactory();
            var mqttSubscribeOptions = mqttFactory.CreateUnsubscribeOptionsBuilder()
                .WithTopicFilter(MqttTopics.CommandsTopic)
                .WithTopicFilter(MqttTopics.StatesTopic)
                .Build();
            await _mqttClient.UnsubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
            _mqttClient.UseApplicationMessageReceivedHandler(_ => { });
            _started = false;
        }

        private Task HandleMessage(MqttApplicationMessageReceivedEventArgs arg)
        {
            DeviceMessageReceived?.Invoke(new DeviceMessage(arg.ApplicationMessage.Topic,arg.ClientId,DateTime.UtcNow));

            return Task.CompletedTask;

        }
    }
}
