using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomiesAPI.Models;

public partial class Offer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? OfferDescription { get; set; } = null!;
    public string? PetSitterDescription { get; set; } = null!;

    public string Host { get; set; } = null!;

    public int AddressId { get; set; }

    public DateTime? DateAdd { get; set; }
    public decimal? Price { get; set; }

    public int? PhotoId { get; set; }

    public virtual Address Address { get; set; } = null!;
    public virtual Photo? Photo { get; set; } = null!;
    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();



    public virtual User HostNavigation { get; set; } = null!;
    //public virtual Photo? PhotoNavigation { get; set; }

    public virtual ICollection<OfferAnimalType> OfferAnimalTypes { get; set; } = new List<OfferAnimalType>();
}
