using System.ComponentModel.DataAnnotations;

namespace FIlmsApp.Models.FormRequest
{
    public class FilmFormRequest : BaseFormRequest
    {
        public int GenreId { get; set; }
        public int ActorId { get; set; }
        [Required]
        public DateTime ReleasedDate { get; set; }
    }
}
