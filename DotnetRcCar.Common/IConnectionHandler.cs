namespace DotnetRcCar.Common;

public interface IConnectionHandler
{

    Task ConnectAsync();
    Task DisconnectAsync();
}