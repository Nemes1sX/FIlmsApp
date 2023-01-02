using System.ComponentModel.DataAnnotations;

namespace FIlmsApp.Models.FormRequest
{
    public abstract class BaseFormRequest
    {
        [Required, MinLength(3), MaxLength(255)]
        public string Name { get; set; }
    }
}
