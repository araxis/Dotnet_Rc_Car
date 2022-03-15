using DotnetRcCar.Common;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace DotnetRcCar.Mobile.Serviceses;

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

            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithClientId("Mobile Client")
                .WithTcpServer(settings.Host, settings.Port)
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