using DevangelicPlayer.Services;
using DevangelicPlayer.ViewModels;
using DevangelicPlayer.Views;
using CommunityToolkit.Maui;

namespace DevangelicPlayer;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().UseMauiCommunityToolkit();
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<ScannerService>();
        builder.Services.AddSingleton<AudioService>();
        builder.Services.AddSingleton<OrganizerService>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();
        return builder.Build();
    }
}
