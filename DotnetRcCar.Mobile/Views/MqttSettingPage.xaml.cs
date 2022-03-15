using DotnetRcCar.Mobile.ViewModels;

namespace DotnetRcCar.Mobile.Views;

public partial class MqttSettingPage : ContentPage
{
	public MqttSettingPage(MqttSettingPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext=viewModel;
	}
}