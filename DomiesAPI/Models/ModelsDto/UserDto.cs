namespace DomiesAPI.Models.ModelsDto
{
    public class UserDto
    {
        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int RoleId { get; set; }

        public DateTime? DateAdd { get; set; }
    }
}
