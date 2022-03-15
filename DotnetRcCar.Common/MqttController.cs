using MQTTnet;
using MQTTnet.Client;

namespace DotnetRcCar.Common;

internal class MqttController:IRemoteController,IConnectionStateTracker
{

    private  string MessagePrefix = "c";
    private  string Up = "up";
    private  string UpLeft = "up/left";
    private  string UpRight = "up/right";
    private  string Back = "back";
    private  string BackLeft = "back/left";
    private  string BackRight = "back/right";
    private  string Left = "left";
    private  string Right = "right";
    private  string StopTopic = "stop";

    public event ControllerStateHandler? StateChanged;
  
    private readonly IMqttClient _mqttClient;

    public MqttController(IMqttClient mqttClient)
    {
        _mqttClient = mqttClient;
        _mqttClient.UseConnectedHandler(_ => OnStateChanged(new ConecctionState(true)));
        _mqttClient.UseDisconnectedHandler(_ => OnStateChanged(new ConecctionState(false)));
    }




    public bool IsConnected => _mqttClient.IsConnected;

    private MqttApplicationMessage GetTopic(string baseTopic)
    {
       var topic= $"{MessagePrefix}/{baseTopic}";
      return new MqttApplicationMessageBuilder()
           .WithTopic(topic)
           .Build();
    } 
    public async Task Forward()
    {
     
        await _mqttClient.PublishAsync(GetTopic(Up), CancellationToken.None);
    }

    public async Task ForwardLeft()
    {
        await _mqttClient.PublishAsync(GetTopic(UpLeft), CancellationToken.None);
    }

    public async Task ForwardRight()
    {
        await _mqttClient.PublishAsync(GetTopic(UpRight), CancellationToken.None);
    }

    public async Task Backward()
    {
        await _mqttClient.PublishAsync(GetTopic(Back), CancellationToken.None);
    }

    public async Task BackwardLeft()
    {
        await _mqttClient.PublishAsync(GetTopic(BackLeft), CancellationToken.None);
    }

    public async Task BackwardRight()
    {
        await _mqttClient.PublishAsync(GetTopic(BackRight), CancellationToken.None);
    }

    public async Task TurnLeft()
    {
        await _mqttClient.PublishAsync(GetTopic(Left), CancellationToken.None);
    }

    public async Task TurnRight()
    {
        await _mqttClient.PublishAsync(GetTopic(Right), CancellationToken.None);
    }

    public async Task Stop()
    {
        await _mqttClient.PublishAsync(GetTopic(StopTopic), CancellationToken.None);
    }


    private async  Task OnStateChanged(ConecctionState state)
    {
        if(StateChanged is { } handler)
         await  handler.Invoke(state);
    }
}