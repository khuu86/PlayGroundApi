using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayGroundLibForberedelse;

namespace PlayGroundApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayGroundsController : Controller
    {
        private readonly PlayGroundsRepository _playGroundsRepository;

        // Constructor der inialiterer PlayGroundsRepository

        public PlayGroundsController(PlayGroundsRepository playGroundsRepository)
        {
            _playGroundsRepository = playGroundsRepository;
        }

        // GET: api/PlayGrounds
        // Henter alle legepladser
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<PlayGround>> GetAll()
        {
            IEnumerable<PlayGround> playGrounds = _playGroundsRepository.GetAll();
            if (playGrounds == null)
            {
                return NotFound();
            }
            return Ok(playGrounds);
        }

        // GET: api/PlayGrounds/5
        // Henter en legeplads med et bestemt id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<PlayGround> Get(int id)
        {
            PlayGround playGround = _playGroundsRepository.GetById(id);
            if (playGround == null)
            {
                return NotFound();
            }
            return Ok(playGround);
        }

        // POST: api/PlayGrounds
        // Tilføjer en ny legeplads
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<PlayGround> Post([FromBody] PlayGround playGround)
        {
            try
            {
                PlayGround createdPlayGround = _playGroundsRepository.Add(playGround);
                return Created("/" + createdPlayGround.Id, createdPlayGround);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/PlayGrounds/5
        // Opdaterer en legeplads baseret på ID
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<PlayGround> Put(int id, [FromBody] PlayGround playGround)
        {
            try
            {
                PlayGround updatedPlayGround = _playGroundsRepository.Update(id, playGround);
                if (updatedPlayGround == null)
                {
                    return NotFound("Playground not found");
                }
                return Ok(updatedPlayGround);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
