using DevangelicPlayer.Models;
namespace DevangelicPlayer.Services;
public class ScannerService
{
    private readonly DatabaseService _db;
    public ScannerService(DatabaseService db) => _db = db;
    public async Task<int> ScanFolderAsync(string folder, IProgress<int> progress = null)
    {
        await _db.InitAsync();
        if (!Directory.Exists(folder)) return 0;
        var exts = new[] { ".mp3", ".flac", ".ogg", ".m4a", ".wav" };
        var files = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories).Where(f => exts.Contains(Path.GetExtension(f).ToLower())).ToList();
        int count = 0;
        foreach (var file in files)
        {
            try
            {
                var song = new Song
                {
                    Path = file,
                    Title = Path.GetFileNameWithoutExtension(file),
                    Artist = "Desconocido",
                    Album = "",
                    Duration = 0,
                    FileSize = new FileInfo(file).Length,
                };
                await _db.AddOrUpdateAsync(song);
                count++; progress?.Report(count);
            } catch { continue; }
        }
        return count;
    }
}
