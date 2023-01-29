using FIlmsApp.Models.FormRequest.Rules;
using System.ComponentModel.DataAnnotations;

namespace FIlmsApp.Models.FormRequest
{
    public class StoreFilmFormRequest : BaseFormRequest
    {
        [ExistingGenre]
        public int? GenreId { get; set; }
        [ExistingActor]
        public int? ActorId { get; set; }
        [Required]
        public DateTime ReleasedDate { get; set; }
    }
}
