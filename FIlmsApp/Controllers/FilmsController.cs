using FIlmsApp.Models.FormRequest;
using FIlmsApp.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FIlmsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FilmsController(IFilmService filmService) 
        {
            _filmService = filmService;
        }

        // GET: api/<FilmsController>/index
        [HttpGet]
        [Route("index")]
        public async Task<ActionResult> Index()
        {
            var films = await _filmService.GetAll();

            if (films == null || films.Count() == 0) 
            {
                return NotFound(new { message = "No films found" });
            }

            return Ok(new {films = films});
        }

        // GET api/<FilmsController>/5
        [HttpGet]
        [Route("id")]
        public async Task<ActionResult> Get(int id)
        {
            var film = await _filmService.Read(id);

            if (film == null)
            {
                return NotFound(new { message = "Selected film not found" });
            }

            return Ok(new { films = film });
        }

        // POST api/<FilmsController>
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> Post(StoreFilmFormRequest filmFormRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Validation Error" });
            }
            var film = await _filmService.Create(filmFormRequest);

            return Ok(new { film = film });
        }

        // PUT api/<FilmsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FilmsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
