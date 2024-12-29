namespace DomiesAPI.Models.ModelsDto
{
    public class ApplicationDto
    {
        public int Id { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int OfferId { get; set; }

        public string ToUser { get; set; } = null!;

        public DateTime? ApplicationDateAdd { get; set; }


    }
}
