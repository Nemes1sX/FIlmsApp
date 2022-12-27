using System.ComponentModel.DataAnnotations.Schema;

namespace FIlmsApp.Models.Entities
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleasedDate { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public ICollection<Actor> Actors { get; set; }
        public Genre Genre { get; set; }
    }
}
