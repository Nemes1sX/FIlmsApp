using FIlmsApp.Data;
using System.ComponentModel.DataAnnotations;

namespace FIlmsApp.Models.FormRequest.Rules
{
    public class ExistingGenre : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var filmFormRequest = (StoreFilmFormRequest)validationContext.ObjectInstance;

            if (filmFormRequest.GenreId == null)
            {
                return ValidationResult.Success;
            }

            var _db = (FilmsContext)validationContext.GetService(typeof(FilmsContext));

            var genre = _db.Films.SingleOrDefault(x => x.Id == filmFormRequest.GenreId);

            return genre == null
                ? new ValidationResult("Selected genre list don't exist")
                : ValidationResult.Success;
        }
    }
}
