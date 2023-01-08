using FIlmsApp.Models.Dtos;

namespace FIlmsApp.Services
{
    public interface IFilmService
    {

        Task<List<FilmDto>> GetAll();
        Task<FilmDto> Create();
        Task<int> Delete(int id);
        Task<FilmDto> Update();
        Task<FilmDto> Read(int id);
    }
}
