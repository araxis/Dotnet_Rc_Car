using System;
using nanoFramework.M2Mqtt;
using nanoFramework.M2Mqtt.Messages;

namespace DotnetRcCar.NfEsp.Services
{
    public class MqttCommandHandler : ICommandHandler, IDisposable
    {

        private const string BaseUp = "up";
        private const string BaseUpLeft = "up/left";
        private const string BaseUpRight = "up/right";
        private const string BaseBack = "back";
        private const string BaseBackLeft = "back/left";
        private const string BaseBackRight = "back/right";
        private const string BaseLeft = "left";
        private const string BaseRight = "right";
        private const string BaseStop = "stop";

        //topics
        private const string Up = "c/up";
        private const string Back = "c/back";
        private const string Left = "c/left";
        private const string Right = "c/right";
        private const string StopTopic = "c/stop";

        private readonly MqttClient _mqttClient;
        private readonly IDeviceController _controller;
        private readonly IDeviceStatePublisher _statePublisher;

        public MqttCommandHandler(MqttClient mqttClient, IDeviceController deviceController, IDeviceStatePublisher statePublisher)
        {
            _mqttClient = mqttClient;
            _controller = deviceController;
            _statePublisher = statePublisher;
            _mqttClient.MqttMsgPublishReceived += MqttMqttMsgPublishReceived;
            _mqttClient.MqttMsgSubscribed += OnSubscribed;
        }



        public void Start()
        {
            var x = _mqttClient.IsConnected;
            if (!_mqttClient.IsConnected) return;
            UnSubscribe();
            _mqttClient.Subscribe(
                new[] { $"{Up}/#", $"{Back}/#", Left, Right, StopTopic },
                new[] {MqttQoSLevel.AtMostOnce,MqttQoSLevel.AtMostOnce,
                   MqttQoSLevel.AtMostOnce,MqttQoSLevel.AtMostOnce,MqttQoSLevel.AtMostOnce});

        }

        private void OnSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Console.WriteLine($"OK;{e.MessageId}");
        }

        public void Stop()
        {
            if (_mqttClient.IsConnected)
            {
                UnSubscribe();
            }



        }

        private void UnSubscribe()
        {
            _mqttClient.Unsubscribe(new[] { $"{Up}/#", $"{Back}/#", Left, Right, StopTopic });
        }



        private void MqttMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            var topic = GetTopic(e.Topic);
            switch (topic)
            {
                case BaseUp:
                    _controller.Forward();
                    break;
                case BaseUpLeft:
                    _controller.ForwardLeft();
                    break;
                case BaseUpRight:
                    _controller.ForwardRight();
                    break;
                case BaseBack:
                    _controller.Backward();
                    break;
                case BaseBackLeft:
                    _controller.BackwardLeft();
                    break;
                case BaseBackRight:
                    _controller.BackwardRight();
                    break;
                case BaseLeft:
                    _controller.Left();
                    break;
                case BaseRight:
                    _controller.Right();
                    break;
                case BaseStop:
                    _controller.Stop();
                    break;

            }

            _statePublisher.Publish(_controller.State);
        }


        private static string GetTopic(string receivedTopic)
        {
            var first = receivedTopic.IndexOf("/");

            if (first == -1) return receivedTopic;

            // Find the "next" occurrence by starting just past the first
            //  var secondIndex= receivedTopic.IndexOf("/", first + 1);
            // return receivedTopic.Substring(secondIndex + 1);
            return receivedTopic.Substring(first + 1);
        }

        public void Dispose()
        {
            _mqttClient.MqttMsgPublishReceived -= MqttMqttMsgPublishReceived;
        }
    }
}