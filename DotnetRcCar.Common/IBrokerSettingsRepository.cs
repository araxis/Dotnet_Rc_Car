namespace DotnetRcCar.Common;

public interface IBrokerSettingsRepository
{
    Task<BrokerSettings?> GetSetting();
    Task SetSetting(BrokerSettings setting);
}