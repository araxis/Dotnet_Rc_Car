using DotnetRcCar.Mobile.Serviceses;

namespace DotnetRcCar.Mobile.Core
{
    public interface IAccelerometerController
    {
        event DirectionChanged? PositionChanged;

        void Start();
        void Stop();
    }
}