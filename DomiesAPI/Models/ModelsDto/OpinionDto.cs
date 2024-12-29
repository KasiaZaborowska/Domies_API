namespace DomiesAPI.Models.ModelsDto
{
    public class OpinionDto
    {
        public int Id { get; set; }

        public int? Rating { get; set; }

        public string? Comment { get; set; }

        public int ApplicationId { get; set; }

        public string UserEmail { get; set; } = null!;

        public DateTime? ApplicationDateAdd { get; set; }
    }
}
