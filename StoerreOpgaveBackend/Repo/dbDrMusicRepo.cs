using StoerreOpgaveBackend.models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StoerreOpgaveBackend.Repo
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<DrMusic> Musics { get; set; }
    }

    public class dbDrMusicRepo
    {
        private readonly AppDbContext _context;

        public dbDrMusicRepo(AppDbContext context)
        {
            _context = context;
        }

        public DrMusic Add(string title, string artist, int duration, int publicationDate)
        {
            var music = new DrMusic
            {
                title = title,
                artist = artist,
                duration = duration,
                publicationDate = publicationDate
            };
            _context.Musics.Add(music);
            _context.SaveChanges();
            return music;
        }

        public List<DrMusic> GetAll()
        {
            return _context.Musics.ToList();
        }

        public List<DrMusic> Search(string? title, string? artist)
        {
            var result = _context.Musics.AsQueryable();

            if (!string.IsNullOrEmpty(title))
                result = result.Where(m => m.title.Contains(title));

            if (!string.IsNullOrEmpty(artist))
                result = result.Where(m => m.artist.Contains(artist));

            return result.ToList();
        }

        public DrMusic? GetById(int id)
        {
            return _context.Musics.FirstOrDefault(m => m.Id == id);
        }

        public bool Delete(int id)
        {
            var music = GetById(id);
            if (music != null)
            {
                _context.Musics.Remove(music);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public DrMusic? Update(int id, string title, string artist, int duration, int publicationDate)
        {
            var music = GetById(id);
            if (music != null)
            {
                music.title = title;
                music.artist = artist;
                music.duration = duration;
                music.publicationDate = publicationDate;
                _context.SaveChanges();
                return music;
            }
            return null;
        }
    }
}
