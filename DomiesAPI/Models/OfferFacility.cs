using System;
using System.Collections.Generic;

namespace DomiesAPI.Models;

public partial class OfferFacility
{
    public int OfferId { get; set; }

    public int FacilitieId { get; set; }

    public virtual Facility Facilitie { get; set; } = null!;

    public virtual Offer Offer { get; set; } = null!;
}
