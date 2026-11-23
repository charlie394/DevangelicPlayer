using DevangelicPlayer.Views;

namespace DevangelicPlayer;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new MainPage();
    }
}
