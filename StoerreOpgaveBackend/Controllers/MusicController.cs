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
        public ActionResult<List<DrMusic>> GetAll([FromQuery] string? title, [FromQuery] string? artist)
        {
            List<DrMusic> musics = _repo.Search(title, artist);

            if (musics == null || musics.Count == 0)
                return NotFound("No music records found");

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
        public ActionResult<DrMusic> Add([FromBody] DrMusic music)
        {
            if (string.IsNullOrEmpty(music.title) || string.IsNullOrEmpty(music.artist))
            {
                return BadRequest("Title and Artist are required");
            }
            DrMusic created = _repo.Add(music.title, music.artist, music.duration, music.publicationDate);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DrMusic> Update(int id, [FromBody] DrMusic music)
        {
            if (string.IsNullOrEmpty(music.title) || string.IsNullOrEmpty(music.artist))
            {
                return BadRequest("Title and Artist are required");
            }
            DrMusic? updated = _repo.Update(id, music.title, music.artist, music.duration, music.publicationDate);
            if (updated == null)
            {
                return NotFound($"No music record found with id {id}");
            }
            return Ok(updated);
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