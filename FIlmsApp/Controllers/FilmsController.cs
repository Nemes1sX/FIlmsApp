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

            //return Ok(new {films = films});
            return Ok(films);
        }

        // GET api/<FilmsController>/get?id=5
        [HttpGet]
        [Route("get")]
        public async Task<ActionResult> Get(int id)
        {
            var film = await _filmService.Read(id);

            if (film.Id == 0)
            {
                return NotFound(new { message = "Selected film not found" });
            }

            //return Ok(new { films = film });
            return Ok(film);
        }

        // POST api/<FilmsController>
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> Post(StoreFilmFormRequest filmFormRequest)
        {
            var film = await _filmService.Create(filmFormRequest);

            //return Ok(new { film = film });
            return Ok(film);
        }

        // PUT api/<FilmsController>/update?id=5
        [HttpPut("update")]
        public async Task<ActionResult> Update(int id, [FromBody] StoreFilmFormRequest filmRequest)
        {
            var film = await _filmService.Update(id, filmRequest);

            if (film == null)
            {
                return NotFound(new { message = "Updating record not found" });
            }

            return Ok(film);
        }

        // DELETE api/<FilmsController>/delete?id=5
        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var deletedId = await _filmService.Delete(id);

            if (deletedId == 0)
            {
                return NotFound(new {message = "Film was not found"});
            }

            return Ok(new { message = "FiIlm was deleted" });
        }
    }
}
