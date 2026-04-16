using Microsoft.AspNetCore.Mvc;
using StoerreOpgaveBackend.Repo;
using StoerreOpgaveBackend.models;

namespace StoerreOpgaveBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicController : ControllerBase
    {
        private DrMusicRepo _repo = new DrMusicRepo();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<DrMusic>> GetAll()
        {
            List<DrMusic> musics = _repo.GetAll();
            if (musics == null || musics.Count == 0)
            {
                return NotFound("No music records found");
            }
            return Ok(musics);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DrMusic> GetById(int id)
        {
            DrMusic? music = _repo.GetById(id);
            if (music == null)
            {
                return NotFound($"No music record found with id {id}");
            }
            return Ok(music);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DrMusic> Add(string Title, string Artist, int Duration, int PublicationDate)
        {
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Artist))
            {
                return BadRequest("Title and Artist are required");
            }
            DrMusic music = _repo.Add(Title, Artist, Duration, PublicationDate);
            return CreatedAtAction(nameof(GetById), new { id = music.Id }, music);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DrMusic> Update(int id, string Title, string Artist, int Duration, int PublicationDate)
        {
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Artist))
            {
                return BadRequest("Title and Artist are required");
            }
            DrMusic? music = _repo.Update(id, Title, Artist, Duration, PublicationDate);
            if (music == null)
            {
                return NotFound($"No music record found with id {id}");
            }
            return Ok(music);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            bool deleted = _repo.Delete(id);
            if (!deleted)
            {
                return NotFound($"No music record found with id {id}");
            }
            return NoContent();
        }
    }
}