using System.ComponentModel.DataAnnotations.Schema;

namespace FIlmsApp.Models.Entities
{
    public class Film : BaseEntity
    {
        public DateTime ReleasedDate { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public ICollection<Actor>? Actors { get; set; }
        public Genre? Genre { get; set; }
    }
}
