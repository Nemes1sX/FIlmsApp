using AutoMapper;
using FIlmsApp.Data;
using FIlmsApp.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FIlmsApp.Services
{
    public class FilmService : BaseService, IFilmService
    {
        public FilmService(FilmsContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public Task<FilmDto> Create()
        {
            throw new NotImplementedException();
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
