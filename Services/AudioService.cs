using DevangelicPlayer.Models;
namespace DevangelicPlayer.Services;
public class AudioService
{
    public Song? Current { get; private set; }
    public bool IsPlaying { get; private set; }
    public event Action<Song>? OnSongChanged;
    public void Play(Song song, string path)
    {
        Current = song;
        IsPlaying = true;
        OnSongChanged?.Invoke(song);
        // Reproductor nativo se conecta en v2, por ahora guarda la canción actual
    }
    public void TogglePlayPause() { IsPlaying = !IsPlaying; }
    public void Stop() { IsPlaying = false; }
}
