using System.Windows.Input;
using DotnetRcCar.Common;
using DotnetRcCar.Mobile.Core;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace DotnetRcCar.Mobile.ViewModels;

public class MqttSettingPageViewModel:BaseViewModel
{


    private readonly IBrokerSettingsRepository _settingRepository;
    private readonly INavigator _navigator;
    public  MqttSettingPageViewModel(IBrokerSettingsRepository settingRepository, INavigator navigator)
    {
        _settingRepository = settingRepository;

        SubmitCommand = new AsyncCommand(Submit);
        var settings =   _settingRepository.GetSetting().ConfigureAwait(false).GetAwaiter().GetResult();
        if (settings is not null)
        {
            Host = settings.Host;
            Port = settings.Port;
        }
        _navigator = navigator;
    }


    public ICommand SubmitCommand { get; }

    private string _host=string.Empty;
    private int _port;

    public string Host
    {
        get => _host;
        set => SetProperty(ref _host,value);
    }

    public int Port
    {
        get => _port;
        set => SetProperty(ref _port,value);
    }



    private async Task Submit()
    {
        if(string.IsNullOrWhiteSpace(Host)) return;
        if(Port<=0) return;
        var setting=new BrokerSettings(Host,Port);
        await _settingRepository.SetSetting(setting);

    }

}