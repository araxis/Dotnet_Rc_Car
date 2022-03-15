using DotnetRcCar.Mobile.Core;
using System.Numerics;

namespace DotnetRcCar.Mobile.Serviceses;

public delegate Task DirectionChanged(DevicePosition value);
public class AccelerometerController : IAccelerometerController
{

    public event DirectionChanged? PositionChanged;
    private SensorSpeed _speed = SensorSpeed.UI;



    private DevicePosition _lasDevicePosition = DevicePosition.XCenterYCenter;

    public AccelerometerController()
    {
        Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
    }

    private void Accelerometer_ReadingChanged(object? sender, AccelerometerChangedEventArgs e)
    {
        var data = e.Reading.Acceleration;
        // Console.WriteLine($"X:{data.X}  Y:{data.Y}");
        var position = ToPosition(data);
        if (position == _lasDevicePosition) return;
        _lasDevicePosition = position;
        OnPositionChanged(_lasDevicePosition);


    }

    private DevicePosition ToPosition(Vector3 data)
    {

        if (XCenter(data.X) && YUp(data.Y)) return DevicePosition.XCenterYUp;
        if (XCenter(data.X) && YDown(data.Y)) return DevicePosition.XCenterYDown;
        if (YCenter(data.Y) && XLeft(data.X)) return DevicePosition.XLeftYCenter;
        if (YCenter(data.Y) && XRight(data.X)) return DevicePosition.XRightYCenter;
        if (XLeft(data.X) && YUp(data.Y)) return DevicePosition.XLeftYUp;
        if (XLeft(data.X) && YDown(data.Y)) return DevicePosition.XLeftYDown;
        if (XRight(data.X) && YUp(data.Y)) return DevicePosition.XRightYUp;
        if (XRight(data.X) && YDown(data.Y)) return DevicePosition.XRightYDown;
        return DevicePosition.XCenterYCenter;
    }

    private bool YCenter(float point) => point <= 0.3 && point >= -0.1;
    private bool XCenter(float point) => point <= 0.2 && point >= -0.3;
    private bool XRight(float point) => point < -0.3;
    private bool XLeft(float point) => point > 0.2;

    private bool YUp(float point) => point > 0.3;
    private bool YDown(float point) => point < -0.1;

    public void Start()
    {

        try
        {
            if (Accelerometer.IsMonitoring)
                return;
            else
                Accelerometer.Start(_speed);
        }
        catch (FeatureNotSupportedException fnsEx)
        {
            // Feature not supported on device
        }
        catch (Exception ex)
        {
            // Other error has occurred.
        }
    }
    public void Stop()
    {
        if (!Accelerometer.IsMonitoring) return;
        Accelerometer.Stop();
    }


    protected virtual void OnPositionChanged(DevicePosition value)
    {
        PositionChanged?.Invoke(value);
    }
}
