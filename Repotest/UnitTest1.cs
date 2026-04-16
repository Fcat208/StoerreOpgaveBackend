using StoerreOpgaveBackend.models;
using StoerreOpgaveBackend.Repo;
using Xunit;

namespace Repotest
{
    public class DrMusicRepoTest
    {
        private DrMusicRepo repo;

        public DrMusicRepoTest()
        {
            repo = new DrMusicRepo();
        }

        [Fact]

        public void testGetAll()
        {
            // Arrange
            repo.Add("Song1", 0, "Artist1", 200, 2000);
            repo.Add("Song2", 0, "Artist2", 300, 2010);
            repo.Add("Song3", 0, "Artist3", 250, 2020);

            // Act
            List<DrMusic> all = repo.GetAll();

            // Assert
            Assert.Equal(3, all.Count);
            Assert.Contains(all, m => m.title == "Song1");
            Assert.Contains(all, m => m.title == "Song2");
            Assert.Contains(all, m => m.title == "Song3");
        }
        [Fact]
        public void testGetById()
        {
            // Arrange
            DrMusic song = repo.Add("Song1", 0, "Artist1", 200, 2000);
            // Act
            DrMusic? result = repo.GetById(song.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(song.Id, result?.Id);
        }
        [Fact]
        public void testDelete()
        {
            // Arrange
            DrMusic song = repo.Add("Song1", 0, "Artist1", 200, 2000);
            // Act
            bool deleted = repo.Delete(song.Id);
            DrMusic? result = repo.GetById(song.Id);
            // Assert
            Assert.True(deleted);
            Assert.Null(result);

        }
        [Fact]
        public void testUpdate()
        {
            // Arrange
            DrMusic song = repo.Add("Song1", 0, "Artist1", 200, 2023);

            // Act
            repo.Update(song.Id, "UpdatedSong", "UpdatedArtist", 210, 2024);
            DrMusic? result = repo.GetById(song.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("UpdatedSong", result?.title);
            Assert.Equal("UpdatedArtist", result?.artist);
            Assert.Equal(210, result?.duration);
            Assert.Equal(2024, result?.publicationDate);
        }

    }
}

        