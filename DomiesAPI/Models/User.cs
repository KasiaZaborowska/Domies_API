using System;
using System.Collections.Generic;

namespace DomiesAPI.Models;

public partial class User
{
    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public DateTime? DateAdd { get; set; }

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();

    public virtual Role Role { get; set; } = null!;
}
