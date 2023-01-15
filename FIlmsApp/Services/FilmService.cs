using AutoMapper;
using FIlmsApp.Data;
using FIlmsApp.Models.Dtos;
using FIlmsApp.Models.Entities;
using FIlmsApp.Models.FormRequest;
using Microsoft.EntityFrameworkCore;

namespace FIlmsApp.Services
{
    public class FilmService : BaseService, IFilmService
    {
        public FilmService(FilmsContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public async Task<FilmDto> Create(FilmFormRequest filmFormRequest)
        {
            var actor = await _db.Actors.FindAsync(filmFormRequest.ActorId);
            var genre = await _db.Genres.FindAsync(filmFormRequest.GenreId);
            if (actor == null || genre == null)
            {
                return null;
            }
            var film = new Film();
            film.Name = filmFormRequest.Name;
            film.ReleasedDate = filmFormRequest.ReleasedDate;
            film.Genre = genre;
            film.Actors = new List<Actor> { actor };

            _db.Films.Add(film);
            await _db.SaveChangesAsync();

            return _mapper.Map<FilmDto>(film);
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FilmDto>> GetAll()
        {
            var films = await _db.Films.Include(x => x.Actors).Include(x => x.Genre).Take(10).ToListAsync();
            var filmsDto = _mapper.Map<List<FilmDto>>(films);

            return filmsDto;
        }

        public Task<FilmDto> Read(int id)
        {
            throw new NotImplementedException();
        }

        public Task<FilmDto> Update()
        {
            throw new NotImplementedException();
        }
    }
}
