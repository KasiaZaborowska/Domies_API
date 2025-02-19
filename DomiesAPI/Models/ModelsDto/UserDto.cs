using System.ComponentModel.DataAnnotations;

namespace DomiesAPI.Models.ModelsDto
{
    public class UserDto
    {
        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Błędny format adresu email.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Imię jest wymagane.")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [MinLength(8, ErrorMessage = "Hasło musi mieć co najmniej 8 znaków.")]
        public string Password { get; set; } = null!;

        public int RoleId { get; set; } = 1;
        public string? RoleName { get; set; }

        public DateTime? DateAdd { get; set; }
    }
}
