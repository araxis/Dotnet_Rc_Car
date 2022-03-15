using Blazored.LocalStorage;
using DotnetRcCar.Common;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MQTTnet;
using MQTTnet.Client;

namespace DotnetRcCar.Web.Services;

public static class Extensions
{
    public static IServiceCollection AddStore(this IServiceCollection services)
    {
        services.AddBlazoredLocalStorage();
        services.AddScoped<IBrokerSettingsRepository, MqttSettingsRepository>();
        return services;
    }


}