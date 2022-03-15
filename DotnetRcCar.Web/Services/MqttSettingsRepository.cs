using Blazored.LocalStorage;
using DotnetRcCar.Common;

namespace DotnetRcCar.Web.Services;

public class MqttSettingsRepository : IBrokerSettingsRepository
{
    private readonly ILocalStorageService _localStorageService;
    private string Key = "Mqtt";

    public MqttSettingsRepository(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }


    public async Task<BrokerSettings?> GetSetting()
    {
        return await _localStorageService.GetItemAsync<BrokerSettings>(Key);
    }

    public async Task SetSetting(BrokerSettings setting)
    {
        await _localStorageService.SetItemAsync(Key, setting);
    }
}