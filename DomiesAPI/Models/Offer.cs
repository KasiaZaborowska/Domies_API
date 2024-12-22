using System;
using System.Collections.Generic;

namespace DomiesAPI.Models;

public partial class Offer
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Photo { get; set; }

    public string Description { get; set; } = null!;

    public string Host { get; set; } = null!;

    public int AddressId { get; set; }

    public DateTime? DateAdd { get; set; }

    public virtual Address Address { get; set; } = null!;
    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();



    public virtual User HostNavigation { get; set; } = null!;
}
