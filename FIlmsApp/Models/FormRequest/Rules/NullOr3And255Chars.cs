using System.ComponentModel.DataAnnotations;

namespace FIlmsApp.Models.FormRequest.Rules
{
    public class NullOr3And255Chars : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var filmFormRequest = (StoreFilmFormRequest)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(filmFormRequest.Name) || string.IsNullOrWhiteSpace(filmFormRequest.Name)) 
            {
                return ValidationResult.Success;
            }

            return filmFormRequest.Name.Length >= 3 || filmFormRequest.Name.Length <= 255
                ? ValidationResult.Success
                : new ValidationResult("Set value must be between 3 and 255 chars");
        }
    }
}
