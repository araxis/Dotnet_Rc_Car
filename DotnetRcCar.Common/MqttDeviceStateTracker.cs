using MQTTnet;
using MQTTnet.Client;

namespace DotnetRcCar.Common;

public class MqttDeviceStateTracker:IDeviceStateTracker
{
    public event DeviceStateHandler? StateChanged;
    public DeviceState DeviceState { get; private set; } = new(WheelState.Stop, WheelState.Stop, 0, 0);

    private readonly IMessageListener _messageListener;
    private bool _started = false;
    public MqttDeviceStateTracker(IMessageListener messageListener)
    {
        _messageListener = messageListener;
    }

    public  Task StartTracking()
    {
        if (_started) return Task.CompletedTask; ;
        _messageListener.DeviceMessageReceived += DeviceMessageReceived;
        _started = true;
        return Task.CompletedTask;
    }

   

    public  Task StopTracking()
    {
        _messageListener.DeviceMessageReceived -= DeviceMessageReceived;
        _started = false;
        return Task.CompletedTask;
    }

    private async Task DeviceMessageReceived(DeviceMessage state)
    {
        var topic = state.Message;
        if (!topic.StartsWith(MqttTopics.StateTopicPrefix)) return;
        var segments = topic.Split("/");
        var left = int.Parse(segments[1]);
        var right = int.Parse(segments[2]);
        var leftDirection = left switch
        {
            < 0 => WheelState.Backward,
            > 0 => WheelState.Forward,
            _ => WheelState.Stop,
        };

        var rightDirection = right switch
        {
            < 0 => WheelState.Backward,
            > 0 => WheelState.Forward,
            _ => WheelState.Stop,
        };

        DeviceState = new DeviceState(leftDirection, rightDirection, Math.Abs(left), Math.Abs(right));
        await OnStateChanged(DeviceState);
    }



    private async  Task OnStateChanged(DeviceState state)
    {
        if(StateChanged is not null)
        {
            await StateChanged.Invoke(state);
        }
    }

}