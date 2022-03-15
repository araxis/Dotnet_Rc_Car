namespace DotnetRcCar.NfEsp.Services
{
    public enum WheelState
    {
        Forward, Backward, Stop
    }
    public class DeviceState
    {
        public DeviceState(WheelState leftWheelDirection, WheelState rightWheelDirection, int leftSpeed, int rightSpeed)
        {
            LeftWheelDirection = leftWheelDirection;
            RightWheelDirection = rightWheelDirection;
            LeftSpeed = leftSpeed;
            RightSpeed = rightSpeed;
        }
        public WheelState LeftWheelDirection { get; init; }
        public WheelState RightWheelDirection { get; init; }
        public int LeftSpeed { get; init; }
        public int RightSpeed { get; init; }
    }
}