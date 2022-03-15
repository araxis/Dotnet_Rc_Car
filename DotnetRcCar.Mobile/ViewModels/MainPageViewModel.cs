using System.Windows.Input;
using DotnetRcCar.Common;
using DotnetRcCar.Mobile.Core;
using MvvmHelpers;
using MvvmCommand = MvvmHelpers.Commands;

namespace DotnetRcCar.Mobile.ViewModels
{
    
    public class MainPageViewModel : BaseViewModel, IDisposable
    {

        private readonly IRemoteController _controller;
        private readonly IConnectionStateTracker _connectionStateTracker;
        private readonly IConnectionHandler _connectionHandler;
        private readonly IAccelerometerController _accelerometerController;
        private bool _isConnected;
        private bool _useAccelerometer;

        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }


         public bool UseAccelerometer
        {
            get => _useAccelerometer;
            set
            {
                SetProperty(ref _useAccelerometer, value);
                HandleAccelerometer(value);
            }
        }

  
        public ICommand ForwardCommand { get;private set; }
        public ICommand ForwardLeftCommand { get;private set; }
        public ICommand ForwardRightCommand { get; private set; }
        public ICommand BackwardCommand { get; private set; }
        public ICommand BackwardLeftCommand { get; private set; }
        public ICommand BackwardRightCommand { get; private set; }
        public ICommand LeftCommand { get; private set; }
        public ICommand RightCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand ConnectCommand { get; private set; }
        public ICommand DisconnectCommand { get; private set; }

        public MainPageViewModel(IRemoteController controller, IAccelerometerController accelerometerController, IConnectionStateTracker connectionStateTracker, IConnectionHandler connectionHandler)
        {

            _controller = controller;
            _accelerometerController=accelerometerController;
            _connectionStateTracker = connectionStateTracker;
            _connectionHandler = connectionHandler;
            _accelerometerController.PositionChanged+=PositionChanged;
            _connectionStateTracker.StateChanged += ConnectionStateChanged;
            ConfigCommands();

        }

 

        private void ConfigCommands()
        {
            ForwardCommand = new MvvmCommand.Command(Forward);
            ForwardLeftCommand = new MvvmCommand.Command(ForwardLeft);
            ForwardRightCommand = new MvvmCommand.Command(ForwardRight);
            ConnectCommand = new MvvmCommand.AsyncCommand(Connect);
            BackwardCommand = new MvvmCommand.Command(BackWard);
            BackwardLeftCommand = new MvvmCommand.Command(BackWardLeft);
            BackwardRightCommand = new MvvmCommand.Command(BackWardRight);
            LeftCommand =new MvvmCommand.Command(Left);
            RightCommand =new MvvmCommand.Command(Right);
            StopCommand =new MvvmCommand.Command(Stop);
            DisconnectCommand = new MvvmCommand.AsyncCommand(Disconnect);
        }

        private async Task Disconnect()
        {
            await _connectionHandler.DisconnectAsync();
        }

        private async Task Connect()
        {
            await _connectionHandler.ConnectAsync();
        }

        private Task ConnectionStateChanged(Common.ConecctionState state)
        {
            IsConnected = state.IsConnected;
            return Task.CompletedTask;
        }

        private async Task PositionChanged(DevicePosition value)
        {
            switch (value)
            {
                case DevicePosition.XCenterYCenter:
                    Stop();
                    break;
                case DevicePosition.XLeftYCenter:
                    Left();
                    break;
                case DevicePosition.XRightYCenter:
                    Right();
                    break;
                case DevicePosition.XCenterYUp:
                    BackWard();
                    break;
                case DevicePosition.XLeftYUp:
                    BackWardLeft();
                    break;
                case DevicePosition.XRightYUp:
                    BackWardRight(); 
                    break;
                case DevicePosition.XCenterYDown:
                    Forward();
                    break;
                case DevicePosition.XLeftYDown:
                    ForwardLeft();
                    break;
                case DevicePosition.XRightYDown:
                    ForwardRight();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            
        }

        private void HandleAccelerometer(bool value)
        {
            if (value)
            {
                _accelerometerController.Start();
            }
            else
            {
                _accelerometerController.Stop();
            }
        }

        private void Forward()
        {
            _controller.Forward();

        }
        private void ForwardLeft()
        {
            _controller.ForwardLeft();

        }
        private void ForwardRight()
        {
            _controller.ForwardRight();

        }
        private void BackWard()
        {
            _controller.Backward();
        }
        private void BackWardLeft()
        {
            _controller.BackwardLeft();
        }
        private void BackWardRight()
        {
            _controller.BackwardRight();
        }
        private void Stop()
        {
            _controller.Stop();
        }
        private void Left()
        {
            _controller.TurnLeft();
        }
        private void Right()
        {
            _controller.TurnRight();
        }

     


        public void Dispose()
        {
            _connectionStateTracker.StateChanged -= ConnectionStateChanged;
            _accelerometerController.PositionChanged -=PositionChanged;
        }
    }
}
