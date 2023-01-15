using FIlmsApp.Data;
using System.ComponentModel.DataAnnotations;

namespace FIlmsApp.Models.FormRequest.Rules
{
    public class ExistingGenre : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var filmFormRequest = (FilmFormRequest)validationContext.ObjectInstance;
            var _db = (FilmsContext)validationContext.GetService(typeof(FilmsContext));

            var genre = _db.Films.Find(filmFormRequest.ActorId);

            return genre == null
                ? new ValidationResult("Selected genre list don't exist")
                : ValidationResult.Success;
        }
    }
}
