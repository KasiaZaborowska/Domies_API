using System.ComponentModel.DataAnnotations;

namespace DomiesAPI.Models.ModelsDto
{
    public class UserDto
    {
        [Required(ErrorMessage = "Email jest wymagany.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Imię jest wymagane.")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        public string Password { get; set; } = null!;

        public int RoleId { get; set; } = 1;
        public string? RoleName { get; set; }

        public DateTime? DateAdd { get; set; }
    }
}
