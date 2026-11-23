using SQLite;
namespace DevangelicPlayer.Models;
[Table("Playlists")]
public class Playlist
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Rule { get; set; } = "";
    public bool IsSmart { get; set; }
}
