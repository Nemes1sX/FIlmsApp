using FIlmsApp.Models.FormRequest.Rules;
using System.ComponentModel.DataAnnotations;

namespace FIlmsApp.Models.FormRequest
{
    public class UpdateFilmRequest 
    {
        [NullOr3And255Chars]
        public string Name { get; set; }

        [ExistingGenre]
        public int GenreId { get; set; }
        [ExistingActor]
        public int ActorId { get; set; }
        [Required]
        public DateTime ReleasedDate { get; set; }
    }
}
