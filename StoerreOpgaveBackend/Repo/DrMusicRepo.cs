using StoerreOpgaveBackend.models;

namespace StoerreOpgaveBackend.Repo
{
    public class DrMusicRepo
    {
        private readonly List<DrMusic> musics = new();
        private int nextId = 1;

        public DrMusic Add(string Title, string Artist, int Duration, int PublicationDate)
        {
            var music = new DrMusic
            {
                //vi laver det på denne måde, fordi hvis der er flere brugere der prøver at oprette en musik på samme tid, så vil de få forskellige Id'er, og ikke det samme, fordi Interlocked.Increment er trådsikker.
                Id = Interlocked.Increment(ref nextId),
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
            //return musics, dette er også en metode, men vi bruger den nedenunder så vi sender en kopi af den rigtige liste, og ikke den rigtige. Det gør vi af sikkerhedsårsager.
            return new List<DrMusic>(musics);
        }

        public List<DrMusic> Search(string? title, string? artist)
        {
            var result = new List<DrMusic>(musics);

            if (!string.IsNullOrEmpty(title))
                result = result.Where(m => m.title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(artist))
                result = result.Where(m => m.artist.Contains(artist, StringComparison.OrdinalIgnoreCase)).ToList();

            return result;
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