using System;
using System.Collections.Generic;

namespace DomiesAPI.Models;

public partial class OfferAnimalType
{
    public int OfferId { get; set; }  

    public int AnimalTypeId { get; set; } 

    public virtual AnimalType AnimalType { get; set; } = null!;

    public virtual Offer Offer { get; set; } = null!;
}
