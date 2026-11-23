using DevangelicPlayer.Models;

namespace DevangelicPlayer.Services;
public class MusicService
{
    public List<Song> GetAllSongs() => new()
    {
        new() { Title="Cuán Grande es Él", Artist="Coro Devangelic", Album="Himnos Eternos", Duration="4:12", Emoji="✝️", CoverColor="#1DB954" },
        new() { Title="Grande es Jehová", Artist="Devangelic Worship", Album="Alabanza", Duration="3:58", Emoji="🔥", CoverColor="#FF6B00" },
        new() { Title="Alabaré a mi Señor", Artist="Coro Juvenil", Album="En Vivo", Duration="3:20", Emoji="🎤", CoverColor="#8B5CF6" },
        new() { Title="Dios Está Aquí", Artist="Adoración", Album="Intimidad", Duration="5:01", Emoji="🕊️", CoverColor="#00BFFF" },
        new() { Title="El Espíritu de Dios", Artist="Pentecostés", Album="Fuego", Duration="4:44", Emoji="🔥", CoverColor="#FF2D55" },
        new() { Title="Sublime Gracia", Artist="Himnos Clásicos", Album="Gracia", Duration="4:02", Emoji="✨", CoverColor="#FFD60A" },
        new() { Title="Yo Te Busco", Artist="Devangelic", Album="Búsqueda", Duration="3:35", Emoji="❤️", CoverColor="#FF375F" },
        new() { Title="Cuan Grande es Dios", Artist="Chris Tomlin Cover", Album="Adoración", Duration="5:20", Emoji="🌍", CoverColor="#30D158" },
    };

    public List<string> GetPlaylists() => new() { "❤️ Favoritos", "🔥 Para Adorar", "🌙 Noche de Oración", "🎤 Coro Domingo", "🏃 Alabanza Activa" };
}
