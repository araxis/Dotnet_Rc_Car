namespace DotnetRcCar.Common;


public record BrokerSettings(string Host, int Port,string Path="mqtt");