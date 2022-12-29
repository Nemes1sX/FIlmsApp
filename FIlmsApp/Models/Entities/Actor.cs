namespace FIlmsApp.Models.Entities
{
    public class Actor : BaseEntity
    {
        public ICollection<Film>? Films { get; set; }
    }
}
