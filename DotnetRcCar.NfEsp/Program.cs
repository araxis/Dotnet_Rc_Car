using System;
using System.Device.Pwm;
using System.Device.WiFi;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;
using DotnetRcCar.NfEsp.Services;
using nanoFramework.Hardware.Esp32;
using nanoFramework.M2Mqtt;
using nanoFramework.M2Mqtt.Messages;
using nanoFramework.Networking;

namespace DotnetRcCar.NfEsp
{
    public class Program
    {


        private const string MySsId = "***";
        private const string MyPassword = "****";

        private const string DeviceId = "DotnetRcCar";
        private const string BrokerIp = "*******";



        //The .net nanoframework does not yet support dependency injection. :(
        private static readonly MqttClient Mqtt = new(BrokerIp);
        private static readonly IDeviceStatePublisher StatePublisher = new MqttDeviceStatePublisher(Mqtt);
        private static readonly IDeviceController DeviceController = new DeviceController();
        private static readonly ICommandHandler CommandHandler = new MqttCommandHandler(Mqtt,DeviceController,StatePublisher);

         static PwmChannel pwmPin = StartConnectionIndicator();
        public static void Main()
        {
           


            CancellationTokenSource cs = new(60000);

            var success = WiFiNetworkHelper.ConnectDhcp(MySsId, MyPassword, WiFiReconnectionKind.Automatic, true, token: cs.Token);

            if (!success)
            {
                Debug.WriteLine($"Can't get a proper IP address and DateTime, error: {NetworkHelper.HelperException}.");
                if (WiFiNetworkHelper.HelperException!= null)
                {
                    Debug.WriteLine($"Exception: {NetworkHelper.HelperException}");
                }

            }
            else
            {

                Debug.WriteLine($"YAY! Connected to Wifi - {MySsId}");
                Debug.WriteLine("IP " + NetworkInterface.GetAllNetworkInterfaces()[0].IPv4Address);

                Mqtt.ProtocolVersion = MqttProtocolVersion.Version_3_1;

                Mqtt.ConnectionOpened += MqttConnectionOpened;

             
                Mqtt.Connect(DeviceId);               
                CommandHandler.Start();
                pwmPin.DutyCycle = 1;


            }

        }

        private static PwmChannel StartConnectionIndicator()
        {
            Configuration.SetPinFunction(2, DeviceFunction.PWM1);
            var pwmPin = PwmChannel.CreateFromPin(2);
            pwmPin.Start();
            pwmPin.DutyCycle = 0;
            return pwmPin;
        }


        private static void MqttConnectionOpened(object sender, ConnectionOpenedEventArgs e)
        {
          
            Console.WriteLine($"Mqtt:{Mqtt.IsConnected}");     
            

        }

    }
}
