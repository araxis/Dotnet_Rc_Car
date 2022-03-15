using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client;

namespace DotnetRcCar.Common;

public static class Extensions
{
    public static IServiceCollection AddMqtt(this IServiceCollection services)
    {
        services.AddSingleton<IMqttClient>(sp =>
        {
            var mqttFactory = new MqttFactory();
            return mqttFactory.CreateMqttClient();
        });

        services.AddSingleton(sp =>
        {
            var client = sp.GetRequiredService<IMqttClient>();
            return new MqttController(client);
        });

        services.AddSingleton<IRemoteController>(sp =>sp.GetRequiredService<MqttController>());
        services.AddSingleton<IConnectionStateTracker>(sp => sp.GetRequiredService<MqttController>());
        services.AddSingleton<IMessageListener, MqttMessageListener>();
        services.AddSingleton<IDeviceStateTracker,MqttDeviceStateTracker>();
        return services;
    }
}