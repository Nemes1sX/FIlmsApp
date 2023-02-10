using System.ComponentModel.DataAnnotations;

namespace FIlmsApp.Models.FormRequest
{
    public abstract class BaseFormRequest
    {
        [Required, StringLength(255, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
