namespace DotnetRcCar.Common;



public enum WheelState
{
   Forward,Backward,Stop
}


public record DeviceState(WheelState LeftWheelDirection,WheelState RightWheelDirection,int LeftSpeed,int RightSpeed);
public delegate Task DeviceStateHandler(DeviceState state);
public interface IDeviceStateTracker
{
    event DeviceStateHandler StateChanged;
    DeviceState DeviceState { get;  }

    Task StartTracking();
    Task StopTracking();
}