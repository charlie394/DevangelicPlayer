using CommunityToolkit.Maui.Views;
namespace DevangelicPlayer.Services;
public class AudioService
{
    private MediaElement _player = new();
    public Models.Song? Current { get; private set; }
    public bool IsPlaying { get; private set; }
    public event Action<Models.Song>? OnSongChanged;
    public void Play(Models.Song song, string path)
    {
        Current = song;
        _player.Source = MediaSource.FromFile(path);
        _player.Play();
        IsPlaying = true;
        OnSongChanged?.Invoke(song);
    }
    public void TogglePlayPause()
    {
        if (IsPlaying) { _player.Pause(); IsPlaying = false; }
        else { _player.Play(); IsPlaying = true; }
    }
    public void Stop() { _player.Stop(); IsPlaying = false; }
    public MediaElement GetElement() => _player;
}
