using FIlmsApp.Models.Entities;

namespace FIlmsApp.Models.Dtos
{
    public class FilmDto : BaseDto
    {
        public DateTime ReleasedDate { get; set; } 
        public ICollection<ActorDto> Actors { get; set; }
        public GenreDto Genre { get; set; }
    }
}
