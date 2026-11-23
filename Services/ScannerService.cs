using DevangelicPlayer.Models;
namespace DevangelicPlayer.Services;
public class ScannerService
{
    private readonly DatabaseService _db;
    public ScannerService(DatabaseService db) => _db = db;
    public async Task<int> ScanFolderAsync(string folder, IProgress<int> progress = null)
    {
        await _db.InitAsync();
        var exts = new[] { ".mp3", ".flac", ".ogg", ".m4a", ".wav" };
        var files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories).Where(f => exts.Contains(Path.GetExtension(f).ToLower())).ToList();
        int count = 0;
        foreach (var file in files)
        {
            try
            {
                using var tagFile = TagLib.File.Create(file);
                var song = new Song
                {
                    Path = file,
                    Title = string.IsNullOrWhiteSpace(tagFile.Tag.Title) ? Path.GetFileNameWithoutExtension(file) : tagFile.Tag.Title,
                    Artist = tagFile.Tag.FirstPerformer ?? "Desconocido",
                    Album = tagFile.Tag.Album ?? "",
                    Genre = tagFile.Tag.FirstGenre ?? "",
                    Year = (int)tagFile.Tag.Year,
                    Track = (int)tagFile.Tag.Track,
                    Duration = tagFile.Properties.Duration.TotalSeconds,
                    FileSize = new FileInfo(file).Length,
                    Bitrate = tagFile.Properties.AudioBitrate
                };
                await _db.AddOrUpdateAsync(song);
                count++; progress?.Report(count);
            } catch { continue; }
        }
        return count;
    }
}
