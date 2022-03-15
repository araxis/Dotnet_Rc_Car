using DotnetRcCar.Mobile.Views;

namespace DotnetRcCar.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            MainPage = new AppShell();
            Routing.RegisterRoute(nameof(MqttSettingPage), typeof(MqttSettingPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
           
        }
    }
}