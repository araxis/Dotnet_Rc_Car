using System.Device.Gpio;
using System.Device.Pwm;
using nanoFramework.Hardware.Esp32;

namespace DotnetRcCar.NfEsp.Services
{
    public class DeviceController : IDeviceController
    {
        public event DeviceStateHandler StateChanged;
        public DeviceState State { get; private set; }
        private readonly GpioController _gpioController = new();
        private const int LSpeed = 13;
        private const int LUp = 12;
        private const int LBack = 14;

        private const int RSpeed = 25;
        private const int RUp = 27;
        private const int RBack = 26;
        private static PwmChannel _lSpeedChannel, _rSpeedChannel;


        public void Forward()
        {

            ConfigPins();
            // Console.WriteLine("F");
            _rSpeedChannel.DutyCycle = 1;
            _lSpeedChannel.DutyCycle = 1;
            _gpioController.Write(LBack, PinValue.Low);
            _gpioController.Write(RBack, PinValue.Low);
            _gpioController.Write(RUp, PinValue.High);
            _gpioController.Write(LUp, PinValue.High);

            State = new DeviceState(WheelState.Forward, WheelState.Forward, 100, 100);
            OnStateChanged(State);
        }

        public void ForwardLeft()
        {

            //  Console.WriteLine("FL");
            ConfigPins();
            _rSpeedChannel.DutyCycle = 1;
            _lSpeedChannel.DutyCycle = 0.4;
            _gpioController.Write(LBack, PinValue.Low);
            _gpioController.Write(RBack, PinValue.Low);
            _gpioController.Write(RUp, PinValue.High);
            _gpioController.Write(LUp, PinValue.High);

            State = new DeviceState(WheelState.Forward, WheelState.Forward, 40, 100);
            OnStateChanged(State);



        }

        public void ForwardRight()
        {
            //Console.WriteLine("FR");
            ConfigPins();

            _rSpeedChannel.DutyCycle = 0.4;
            _lSpeedChannel.DutyCycle = 1;
            _gpioController.Write(LBack, PinValue.Low);
            _gpioController.Write(RBack, PinValue.Low);
            _gpioController.Write(RUp, PinValue.High);
            _gpioController.Write(LUp, PinValue.High);
            State = new DeviceState(WheelState.Forward, WheelState.Forward, 100, 40);
            OnStateChanged(State);


        }

        public void Backward()
        {
            // Console.WriteLine("B");
            ConfigPins();
            _rSpeedChannel.DutyCycle = 1;
            _lSpeedChannel.DutyCycle = 1;
            _gpioController.Write(LBack, PinValue.High);
            _gpioController.Write(RBack, PinValue.High);
            _gpioController.Write(LUp, PinValue.Low);
            _gpioController.Write(RUp, PinValue.Low);

            State = new DeviceState(WheelState.Backward, WheelState.Backward, 100, 100);
            OnStateChanged(State);

        }

        public void BackwardLeft()
        {
            //Console.WriteLine("BL");
            ConfigPins();
            _rSpeedChannel.DutyCycle = 1;
            _lSpeedChannel.DutyCycle = 0.4;
            _gpioController.Write(LBack, PinValue.High);
            _gpioController.Write(RBack, PinValue.High);
            _gpioController.Write(LUp, PinValue.Low);
            _gpioController.Write(RUp, PinValue.Low);

            State = new DeviceState(WheelState.Backward, WheelState.Backward, 40, 100);
            OnStateChanged(State);


        }

        public void BackwardRight()
        {
            //Console.WriteLine("BR");
            ConfigPins();
            _rSpeedChannel.DutyCycle = 0.4;
            _lSpeedChannel.DutyCycle = 1;
            _gpioController.Write(LBack, PinValue.High);
            _gpioController.Write(RBack, PinValue.High);
            _gpioController.Write(LUp, PinValue.Low);
            _gpioController.Write(RUp, PinValue.Low);

            State = new DeviceState(WheelState.Backward, WheelState.Backward, 100, 40);
            OnStateChanged(State);

        }
        public void Right()
        {
            // Console.WriteLine("R");
            ConfigPins();
            // LspeedChannel.DutyCycle = .6;
            // RspeedChannel.DutyCycle = .4;
            _gpioController.Write(LBack, PinValue.Low);
            _gpioController.Write(RBack, PinValue.Low);
            _gpioController.Write(LUp, PinValue.High);
            _gpioController.Write(RUp, PinValue.Low);

            State = new DeviceState(WheelState.Forward, WheelState.Stop, 100, 0);
            OnStateChanged(State);

        }


        public void Left()
        {
            //  Console.WriteLine("L");
            ConfigPins();

            // RspeedChannel.DutyCycle =.6;
            //LspeedChannel.DutyCycle = .4;
            _gpioController.Write(LBack, PinValue.Low);
            _gpioController.Write(RBack, PinValue.Low);
            _gpioController.Write(LUp, PinValue.Low);
            _gpioController.Write(RUp, PinValue.High);

            State = new DeviceState(WheelState.Stop, WheelState.Forward, 0, 100);
            OnStateChanged(State);

        }


        public void Stop()
        {
            // Console.WriteLine("S");
            ConfigPins();
            _gpioController.Write(LBack, PinValue.Low);
            _gpioController.Write(RBack, PinValue.Low);
            _gpioController.Write(LUp, PinValue.Low);
            _gpioController.Write(RUp, PinValue.Low);

            State = new DeviceState(WheelState.Stop, WheelState.Stop, 0, 0);
            OnStateChanged(State);

        }

        //The constructor method should call this method, but there are errors regarding GpioController
        //So, it is currently called by the caller method. 
        private void ConfigPins()
        {
            if (_gpioController.IsPinOpen(LUp)) return;
            _gpioController.OpenPin(LUp, PinMode.Output);
            _gpioController.OpenPin(LBack, PinMode.Output);
            _gpioController.OpenPin(RUp, PinMode.Output);
            _gpioController.OpenPin(RBack, PinMode.Output);
            Configuration.SetPinFunction(LSpeed, DeviceFunction.PWM2);
            _lSpeedChannel = PwmChannel.CreateFromPin(LSpeed);
            _lSpeedChannel.Start();
            Configuration.SetPinFunction(RSpeed, DeviceFunction.PWM3);
            _rSpeedChannel = PwmChannel.CreateFromPin(RSpeed);
            _rSpeedChannel.Start();
            _lSpeedChannel.DutyCycle = 1;
            _rSpeedChannel.DutyCycle = 1;
        }

        private void OnStateChanged(DeviceState state)
        {
            StateChanged?.Invoke(state);
        }
    }
}