namespace DomiesAPI.Models
{
    public class Facility
    {
        public int Id { get; set; }

        public string FacilitiesType { get; set; } = null!;

        public string FacilitiesDescription { get; set; } = null!;
    }
}
