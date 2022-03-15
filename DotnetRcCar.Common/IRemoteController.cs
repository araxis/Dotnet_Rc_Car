namespace DotnetRcCar.Common;


public interface IRemoteController
{

    Task Forward();
    Task ForwardLeft();
    Task ForwardRight();
    Task Backward();
    Task BackwardLeft();
    Task BackwardRight();
    Task TurnLeft();
    Task TurnRight();
    Task Stop();

}