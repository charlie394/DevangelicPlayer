using DevangelicPlayer.ViewModels;
namespace DevangelicPlayer.Views;
public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MainViewModel vm)
            await vm.LoadCommand.ExecuteAsync(null);
    }
}
