using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetRcCar.Common;


public record DeviceMessage(string Message, string ClientId, DateTime Date);
public delegate Task DeviceMessageReceived(DeviceMessage state);
public interface IMessageListener
{
    event DeviceMessageReceived DeviceMessageReceived;

    Task Start();
    Task Stop();
}

