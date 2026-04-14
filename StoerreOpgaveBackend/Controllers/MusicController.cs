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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<List<DrMusic>> GetAll()
        {
            return Ok(_repo.GetAll());
        }
    }
}