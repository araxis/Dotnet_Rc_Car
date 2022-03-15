namespace DotnetRcCar.Common
{
    internal class MqttTopics
    {
        public const string StateTopicPrefix = "s";
        public const string CommandTopicPrefix = "c";
        public const string StatesTopic = $"{StateTopicPrefix}/#";
        public const string CommandsTopic = $"{CommandTopicPrefix}/#";
    }
}
