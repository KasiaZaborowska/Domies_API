namespace DomiesAPI.Models
{
    public class FacilityDto
    {
        public int Id { get; set; }

        public string FacilitiesType { get; set; } = null!;

        public string FacilitiesDescription { get; set; } = null!;
        //public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
    }
}
