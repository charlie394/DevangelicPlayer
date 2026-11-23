using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DevangelicPlayer.Models;
using DevangelicPlayer.Services;

namespace DevangelicPlayer.ViewModels;
public class MainViewModel : INotifyPropertyChanged
{
    private readonly MusicService _service;
    public ObservableCollection<Song> Songs { get; set; } = new();
    public ObservableCollection<Song> FilteredSongs { get; set; } = new();
    public ObservableCollection<string> Playlists { get; set; } = new();

    private Song? _currentSong;
    public Song? CurrentSong { get => _currentSong; set { _currentSong = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasSong)); } }
    public bool HasSong => CurrentSong != null;

    private bool _isPlaying;
    public bool IsPlaying { get => _isPlaying; set { _isPlaying = value; OnPropertyChanged(); OnPropertyChanged(nameof(PlayIcon)); } }
    public string PlayIcon => IsPlaying ? "⏸" : "▶";

    private double _progress = 0.35;
    public double Progress { get => _progress; set { _progress = value; OnPropertyChanged(); } }

    private string _searchText = "";
    public string SearchText { get => _searchText; set { _searchText = value; OnPropertyChanged(); Filter(); } }

    private string _greeting = "Buenas noches";
    public string Greeting { get => _greeting; set { _greeting = value; OnPropertyChanged(); } }

    public ICommand PlayPauseCommand { get; }
    public ICommand NextCommand { get; }
    public ICommand PrevCommand { get; }
    public ICommand SelectSongCommand { get; }
    public ICommand ToggleFavoriteCommand { get; }
    public ICommand ShuffleCommand { get; }

    public MainViewModel(MusicService service)
    {
        _service = service;
        foreach (var s in _service.GetAllSongs()) { Songs.Add(s); FilteredSongs.Add(s); }
        foreach (var p in _service.GetPlaylists()) Playlists.Add(p);
        CurrentSong = Songs.FirstOrDefault();
        Greeting = DateTime.Now.Hour < 12 ? "Buenos días ☀️" : DateTime.Now.Hour < 19 ? "Buenas tardes 🌤️" : "Buenas noches 🌙";

        PlayPauseCommand = new Command(() => IsPlaying = !IsPlaying);
        NextCommand = new Command(Next);
        PrevCommand = new Command(Prev);
        SelectSongCommand = new Command<Song>(s => { if (s != null) { CurrentSong = s; IsPlaying = true; foreach (var x in Songs) x.IsPlaying = x == s; OnPropertyChanged(nameof(FilteredSongs)); } });
        ToggleFavoriteCommand = new Command<Song>(s => { if (s != null) s.IsFavorite = !s.IsFavorite; });
        ShuffleCommand = new Command(() => { var rnd = new Random(); var list = Songs.OrderBy(_ => rnd.Next()).ToList(); FilteredSongs.Clear(); foreach (var s in list) FilteredSongs.Add(s); });
    }

    void Filter()
    {
        FilteredSongs.Clear();
        var q = SearchText?.ToLower() ?? "";
        var list = string.IsNullOrWhiteSpace(q) ? Songs : Songs.Where(s => s.Title.ToLower().Contains(q) || s.Artist.ToLower().Contains(q));
        foreach (var s in list) FilteredSongs.Add(s);
    }
    void Next() { if (CurrentSong == null) return; var i = Songs.IndexOf(CurrentSong); CurrentSong = Songs[(i + 1) % Songs.Count]; IsPlaying = true; }
    void Prev() { if (CurrentSong == null) return; var i = Songs.IndexOf(CurrentSong); CurrentSong = Songs[(i - 1 + Songs.Count) % Songs.Count]; IsPlaying = true; }

    public event PropertyChangedEventHandler? PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string n = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
}
