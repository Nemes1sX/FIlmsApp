using FIlmsApp.Data;
using FIlmsApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIlmsApp.Services
{
    public class FilmService : BaseService, IFilmService
    {
        public FilmService(FilmsContext db) : base(db)
        {
        }

        public Task<Film> Create()
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Film>> GetAll()
        {
            var films = await _db.Films.ToListAsync();

            return films;
        }

        public Task<Film> Read(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Film> Update()
        {
            throw new NotImplementedException();
        }
    }
}
