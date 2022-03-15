using DotnetRcCar.NfEsp.Services;

namespace DotnetRcCar.NfEsp
{


    public interface IDeviceStatePublisher
    {
        void Publish(DeviceState state);
    }
}