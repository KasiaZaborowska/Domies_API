using System.ComponentModel.DataAnnotations;

namespace DomiesAPI.Models.ModelsDto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email jest wymagany.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        public string Password { get; set; } = null!;
    }
}
