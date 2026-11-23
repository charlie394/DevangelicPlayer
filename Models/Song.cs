namespace DevangelicPlayer.Models;
public class Song
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = "";
    public string Artist { get; set; } = "";
    public string Album { get; set; } = "";
    public string Duration { get; set; } = "3:45";
    public string CoverColor { get; set; } = "#1DB954";
    public string Emoji { get; set; } = "🎵";
    public string FilePath { get; set; } = "";
    public bool IsFavorite { get; set; }
    public bool IsPlaying { get; set; }
}
