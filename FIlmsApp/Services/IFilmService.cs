using FIlmsApp.Models.Dtos;
using FIlmsApp.Models.FormRequest;

namespace FIlmsApp.Services
{
    public interface IFilmService
    {

        Task<List<FilmDto>> GetAll();
        Task<FilmDto> Create(StoreFilmFormRequest filmFormRequest);
        Task<int> Delete(int id);
        Task<FilmDto> Update(int id, UpdateFilmRequest filmFormRequest);
        Task<FilmDto> Read(int id);
    }
}
