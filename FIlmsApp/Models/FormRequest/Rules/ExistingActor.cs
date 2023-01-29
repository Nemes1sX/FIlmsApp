using FIlmsApp.Data;
using System.ComponentModel.DataAnnotations;

namespace FIlmsApp.Models.FormRequest.Rules
{
    public class ExistingActor : ValidationAttribute
    {
        protected override  ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var filmFormRequest = (StoreFilmFormRequest) validationContext.ObjectInstance;
            var _db = (FilmsContext)validationContext.GetService(typeof(FilmsContext));

            if (filmFormRequest.ActorId == null)
            {
                return ValidationResult.Success;
            }

            var actors = _db.Films.Where(x => x.Id == filmFormRequest.ActorId).ToList();

            return actors.Count == 0 || actors == null
                ? new ValidationResult("Selected actor list don't exist")
                : ValidationResult.Success;
        }
    }
}
