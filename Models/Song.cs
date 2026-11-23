using SQLite;
namespace DevangelicPlayer.Models;
[Table("Songs")]
public class Song
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    public string Path { get; set; } = "";
    public string Title { get; set; } = "Desconocido";
    public string Artist { get; set; } = "Desconocido";
    public string Album { get; set; } = "";
    public string Genre { get; set; } = "";
    public int Year { get; set; }
    public int Track { get; set; }
    public double Duration { get; set; }
    public int Rating { get; set; }
    public int PlayCount { get; set; }
    public DateTime LastPlayed { get; set; }
    public string CoverPath { get; set; } = "";
    public long FileSize { get; set; }
    public int Bitrate { get; set; }
}
