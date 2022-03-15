namespace DotnetRcCar.Common;

public record ConecctionState(bool IsConnected);
public delegate Task ControllerStateHandler(ConecctionState state);
public interface IConnectionStateTracker
{
    event ControllerStateHandler StateChanged;
    public bool IsConnected { get; }
}