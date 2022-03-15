using nanoFramework.M2Mqtt;
using nanoFramework.M2Mqtt.Messages;

namespace DotnetRcCar.NfEsp.Services
{
    public class MqttDeviceStatePublisher : IDeviceStatePublisher
    {
        private readonly MqttClient _mqttClient;
        private string baseTopic = "s";

        public MqttDeviceStatePublisher(MqttClient mqttClient)
        {
            _mqttClient = mqttClient;
        }

        public void Publish(DeviceState state)
        {
            var leftSpeed = state.LeftWheelDirection switch
            {
                WheelState.Stop => 0,
                WheelState.Forward => state.LeftSpeed,
                _ => -state.LeftSpeed
            };
            var rightSpeed = state.RightWheelDirection switch
            {
                WheelState.Stop => 0,
                WheelState.Forward => state.RightSpeed,
                _ => -state.RightSpeed
            };

            var topic = $"{baseTopic}/{leftSpeed}/{rightSpeed}";
            _mqttClient.Publish(topic, new byte[0], MqttQoSLevel.AtMostOnce, false);
        }
    }
}