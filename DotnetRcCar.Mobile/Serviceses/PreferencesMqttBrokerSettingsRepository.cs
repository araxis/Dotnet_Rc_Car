using DotnetRcCar.Common;
using Newtonsoft.Json;

namespace DotnetRcCar.Mobile.Serviceses;

public class PreferencesMqttBrokerSettingsRepository : IBrokerSettingsRepository
{


    private const string SettingKey = "MQTT";




    public Task<BrokerSettings?> GetSetting()
    {
        var settingString = Preferences.Get(SettingKey, "");
        var result = JsonConvert.DeserializeObject<BrokerSettings?>(settingString);
        return Task.FromResult(result);
    }

    public Task SetSetting(BrokerSettings setting)
    {
        var settingString = JsonConvert.SerializeObject(setting);
        Preferences.Set(SettingKey, settingString);
        return Task.CompletedTask;
    }
}