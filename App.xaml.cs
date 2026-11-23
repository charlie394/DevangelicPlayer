using DevangelicPlayer.Views;

namespace DevangelicPlayer;

public partial class App : Application
{
    public App(MainPage page)
    {
        InitializeComponent();
        MainPage = page;
    }
}
