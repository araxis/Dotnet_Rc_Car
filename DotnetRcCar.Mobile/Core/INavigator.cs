namespace DotnetRcCar.Mobile.Core;

public interface INavigator
{
    Task GoToSettingPage();
    Task ShowAlert(string title, string messge, string cancel);
    Task GoBack();
}
