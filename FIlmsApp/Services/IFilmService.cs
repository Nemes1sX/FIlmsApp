using FIlmsApp.Models.Dtos;
using FIlmsApp.Models.FormRequest;

namespace FIlmsApp.Services
{
    public interface IFilmService
    {

        Task<List<FilmDto>> GetAll();
        Task<FilmDto> Create(FilmFormRequest filmFormRequest);
        Task<int> Delete(int id);
        Task<FilmDto> Update();
        Task<FilmDto> Read(int id);
    }
}
