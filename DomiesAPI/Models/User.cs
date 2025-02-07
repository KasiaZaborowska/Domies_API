using System;
using System.Collections.Generic;

namespace DomiesAPI.Models;

public partial class User
{
    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;
    public bool? IsEmailVerified { get; set; }
    public string? EmailVerificationToken { get; set; }

    public int RoleId { get; set; } = 1;

    public DateTime? DateAdd { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
    public virtual ICollection<Opinion> Opinions { get; set; } = new List<Opinion>();

    public virtual Role Role { get; set; } = null!;
}
