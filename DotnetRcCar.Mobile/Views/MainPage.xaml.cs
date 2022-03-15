using DotnetRcCar.Mobile.ViewModels;

namespace DotnetRcCar.Mobile.Views
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }

       
    }
}