using DotnetRcCar.NfEsp.Services;

namespace DotnetRcCar.NfEsp
{
    public delegate void DeviceStateHandler(DeviceState state);
    public interface IDeviceController
    {
        event DeviceStateHandler StateChanged;
        DeviceState State { get; }
        void Forward();
        void ForwardLeft();
        void ForwardRight();
        void Backward();
        void BackwardLeft();
        void BackwardRight();
        void Right();
        void Left();
        void Stop();
    }
}