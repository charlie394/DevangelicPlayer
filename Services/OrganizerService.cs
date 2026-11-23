namespace DevangelicPlayer.Services;
public class OrganizerService
{
    public string GetOrganizedPath(string baseFolder, Models.Song song)
    {
        string Safe(string s) => string.Join("_", s.Split(Path.GetInvalidFileNameChars()));
        var artist = Safe(string.IsNullOrWhiteSpace(song.Artist) ? "Desconocido" : song.Artist);
        var album = Safe(string.IsNullOrWhiteSpace(song.Album) ? "Sin Album" : song.Album);
        var fileName = $"{song.Track:D2} - {Safe(song.Title)}{Path.GetExtension(song.Path)}";
        return Path.Combine(baseFolder, artist, album, fileName);
    }
}
