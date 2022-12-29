namespace FIlmsApp.Models.Entities
{
    public class Genre : BaseEntity
    {
        public ICollection<Film>? Films { get; set; }
    }
}
