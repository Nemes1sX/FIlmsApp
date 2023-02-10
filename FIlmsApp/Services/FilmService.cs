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

        public async Task<FilmDto> Create(StoreFilmFormRequest filmFormRequest)
        {
            var actor = await _db.Actors.FindAsync(filmFormRequest.ActorId);
            var genre = await _db.Genres.FindAsync(filmFormRequest.GenreId);

            var film = new Film();
            film.Name = filmFormRequest.Name;
            film.ReleasedDate = filmFormRequest.ReleasedDate == DateTime.MinValue ? filmFormRequest.ReleasedDate : DateTime.Today;
            film.Genre = genre;
            film.Actors = new List<Actor> { actor };

            _db.Films.Add(film);
            await _db.SaveChangesAsync();

            return  film != null
                ? _mapper.Map<FilmDto>(film)
                : null;
        }

        public async Task<int> Delete(int id)
        {
            var film = await _db.Films.FindAsync(id);
            if (film == null)
            {
                return 0;
            }

            _db.Films.Remove(film);
            await _db.SaveChangesAsync();

            return id;
        }

        public async Task<List<FilmDto>> GetAll()
        {
            var films = await _db.Films.Include(x => x.Actors).Include(x => x.Genre).Take(10).ToListAsync();

            return _mapper.Map<List<FilmDto>>(films);
        }

        public async Task<FilmDto> Read(int id)
        {
            var film = await _db.Films.FindAsync(id);
            if (film == null)
            {
                return new FilmDto();
            }

            return _mapper.Map<FilmDto>(film);
        }

        public async Task<FilmDto> Update(int id, StoreFilmFormRequest filmFormRequest)
        {
            var film = await _db.Films.FindAsync(id);

            if (film == null)
            {
                return null;
            }

            var actor = await _db.Actors.FindAsync(filmFormRequest.ActorId);
            var genre = await _db.Genres.FindAsync(filmFormRequest.GenreId);


            film.Name = filmFormRequest.Name;
            film.ReleasedDate = filmFormRequest.ReleasedDate == DateTime.MinValue ? filmFormRequest.ReleasedDate : DateTime.Today;
            if (actor != null)
            {
                film.Actors = new List<Actor> { actor};
            }
            if (genre != null)
            {
                film.Genre = genre;
            }

            await _db.SaveChangesAsync();

            return _mapper.Map<FilmDto>(film);
        }
    }
}
