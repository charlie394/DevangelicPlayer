using SQLite;
using DevangelicPlayer.Models;
namespace DevangelicPlayer.Services;
public class DatabaseService
{
    private SQLiteAsyncConnection _db;
    public async Task InitAsync()
    {
        if (_db != null) return;
        var path = Path.Combine(FileSystem.AppDataDirectory, "library.db3");
        _db = new SQLiteAsyncConnection(path);
        await _db.CreateTableAsync<Song>();
        await _db.CreateTableAsync<Playlist>();
    }
    public async Task<List<Song>> GetAllAsync() => await _db.Table<Song>().OrderBy(s => s.Artist).ThenBy(s => s.Album).ThenBy(s => s.Track).ToListAsync();
    public async Task<List<Song>> SearchAsync(string q) => await _db.QueryAsync<Song>("SELECT * FROM Songs WHERE Title LIKE ? OR Artist LIKE ? OR Album LIKE ?", $"%{q}%", $"%{q}%", $"%{q}%");
    public async Task AddOrUpdateAsync(Song s)
    {
        var existing = await _db.Table<Song>().Where(x => x.Path == s.Path).FirstOrDefaultAsync();
        if (existing == null) await _db.InsertAsync(s);
        else { s.Id = existing.Id; await _db.UpdateAsync(s); }
    }
    public async Task IncrementPlayCountAsync(int id)
    {
        var s = await _db.Table<Song>().Where(x => x.Id == id).FirstOrDefaultAsync();
        if (s != null) { s.PlayCount++; s.LastPlayed = DateTime.Now; await _db.UpdateAsync(s); }
    }
}
