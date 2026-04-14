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
    }
}

        