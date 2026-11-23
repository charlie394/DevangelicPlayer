using DevangelicPlayer.ViewModels;
namespace DevangelicPlayer.Views;
public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
