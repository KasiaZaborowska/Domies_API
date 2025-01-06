namespace DomiesAPI.Models
{
    public class Photo
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Extension { get; set; } = null!;

        public string Type { get; set; } = null!;

        public byte[]? BinaryData { get; set; }

        public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
    }
}
