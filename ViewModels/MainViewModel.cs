using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevangelicPlayer.Models;
using DevangelicPlayer.Services;
using System.Collections.ObjectModel;
namespace DevangelicPlayer.ViewModels;
public partial class MainViewModel : ObservableObject
{
    private readonly DatabaseService _db;
    private readonly ScannerService _scanner;
    private readonly AudioService _audio;
    [ObservableProperty] ObservableCollection<Song> songs = new();
    [ObservableProperty] string searchText = "";
    [ObservableProperty] Song? selectedSong;
    [ObservableProperty] string status = "Listo";
    [ObservableProperty] bool isPlaying;
    public MainViewModel(DatabaseService db, ScannerService scanner, AudioService audio)
    {
        _db = db; _scanner = scanner; _audio = audio;
        _audio.OnSongChanged += s => SelectedSong = s;
    }
    [RelayCommand] async Task LoadAsync()
    {
        await _db.InitAsync();
        var all = await _db.GetAllAsync();
        Songs = new ObservableCollection<Song>(all);
        Status = $"{all.Count} canciones";
    }
    [RelayCommand] async Task SearchAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchText)) { await LoadAsync(); return; }
        var res = await _db.SearchAsync(SearchText);
        Songs = new ObservableCollection<Song>(res);
    }
    [RelayCommand] async Task ScanAsync()
    {
        string path = "/storage/emulated/0/Music";
#if WINDOWS
        path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
#endif
        Status = "Escaneando...";
        var count = await _scanner.ScanFolderAsync(path);
        await LoadAsync();
        Status = $"Escaneadas {count} canciones";
    }
    [RelayCommand] async Task PlayAsync(Song song)
    {
        if (song == null) return;
        _audio.Play(song, song.Path);
        IsPlaying = true;
        await _db.IncrementPlayCountAsync(song.Id);
    }
    [RelayCommand] void TogglePlay() { _audio.TogglePlayPause(); IsPlaying = _audio.IsPlaying; }
    [RelayCommand] async Task OrganizeAsync() { Status = "Organizar listo"; await Task.Delay(500); }
}
