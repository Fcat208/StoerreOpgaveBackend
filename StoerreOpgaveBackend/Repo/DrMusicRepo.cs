using StoerreOpgaveBackend.models;
namespace StoerreOpgaveBackend.Repo
{
    public class DrMusicRepo
    {
        private readonly List<DrMusic> musics = new();
        private int nextId = 1;
        
        public DrMusic Add(string Title, int Id, string Artist, int Duration, int PublicationDate)
            {
            var music = new DrMusic
            {
                Id = nextId++,
                title = Title,
                artist = Artist,
                duration = Duration,
                publicationDate = PublicationDate
            };
            musics.Add(music);
            return music;
        }
        public List<DrMusic> GetAll()
        {
            return musics;
        }
        public DrMusic? GetById(int id)
        {
            return musics.FirstOrDefault(m => m.Id == id);
        }
         public bool Delete(int id)
        {
            var music = GetById(id);
            if (music != null)
            {
                musics.Remove(music);
                return true;
            }
            return false;

        }
        public DrMusic? Update(int id, string Title, string Artist, int Duration, int PublicationDate)
        {
            var music = GetById(id);
            if (music != null)
            {
                music.title = Title;
                music.artist = Artist;
                music.duration = Duration;
                music.publicationDate = PublicationDate;
                return music;
            }
            return null;
        }

    }
}
