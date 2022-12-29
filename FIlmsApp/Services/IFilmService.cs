using FIlmsApp.Models.Entities;

namespace FIlmsApp.Services
{
    public interface IFilmService
    {
        Task<List<Film>> GetAll();
        Task<Film> Create();
        Task<int> Delete(int id);
        Task<Film> Update();
        Task<Film> Read(int id);
    }
}
