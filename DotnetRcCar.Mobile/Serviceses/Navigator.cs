using DotnetRcCar.Mobile.Core;
using DotnetRcCar.Mobile.Views;

namespace DotnetRcCar.Mobile.Serviceses;

public class Navigator : INavigator
{
    public async Task GoToSettingPage() => await Shell.Current.GoToAsync(nameof(MqttSettingPage));
    public async Task ShowAlert(string title, string message, string cancel) => await Shell.Current.DisplayAlert(title, message, cancel);

    public async Task GoBack() => await Shell.Current.GoToAsync("..");
}
