using DotnetRcCar.Common;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace DotnetRcCar.Web.Services;

public class MqttConnectionHandler : IConnectionHandler
{
    private readonly IMqttClient _mqttClient;
    private readonly IBrokerSettingsRepository _repository;
    public MqttConnectionHandler(IMqttClient mqttClient, IBrokerSettingsRepository repository)
    {
        _mqttClient = mqttClient;
        _repository = repository;
    }


    public async Task ConnectAsync()
    {
        try
        {
            var settings = await _repository.GetSetting();
            var path = $"ws://{settings.Host}:{settings.Port}/{settings.Path}";
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithClientId("Web Client")
                .WithWebSocketServer(path)
                .Build();


            await _mqttClient.ConnectAsync(mqttClientOptions);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DisconnectAsync()
    {
        await _mqttClient.DisconnectAsync();
    }
}