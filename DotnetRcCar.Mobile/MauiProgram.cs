
using CommunityToolkit.Maui;
using DotnetRcCar.Common;
using DotnetRcCar.Mobile.Core;
using DotnetRcCar.Mobile.Serviceses;
using DotnetRcCar.Mobile.ViewModels;
using DotnetRcCar.Mobile.Views;

namespace DotnetRcCar.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

           // builder.Services.AddMqtt2();
           builder.Services
               .AddMqtt()
               .AddSingleton<INavigator,Navigator>()
               .AddSingleton<IBrokerSettingsRepository,PreferencesMqttBrokerSettingsRepository>()
               .AddTransient<IAccelerometerController,AccelerometerController>()
               .AddSingleton<IConnectionHandler,MqttConnectionHandler>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MqttSettingPageViewModel>();
          
       
            

            return builder.Build();
        }
    }
}