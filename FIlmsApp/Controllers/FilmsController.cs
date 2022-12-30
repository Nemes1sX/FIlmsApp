﻿using FIlmsApp.Services;
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
        public async Task<ActionResult> Get()
        {
            var films = await _filmService.GetAll();

            if (films == null || films.Count() == 0) 
            {
                return NotFound(new { message = "No films found" });
            }

            return Ok(new {films = films});
        }

        // GET api/<FilmsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FilmsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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