namespace DomiesAPI.Models
{
    public class OfferFacility
    {
        public int OfferId { get; set; }

        public int FacilitieId { get; set; }

        public virtual Facility Facilitie { get; set; } = null!;

        public virtual Offer Offer { get; set; } = null!;
    }
}
