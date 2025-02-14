using System.ComponentModel.DataAnnotations;

namespace DomiesAPI.Models.ModelsDto
{
    public class OpinionDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ocena jest wymagana.")]
        public int? Rating { get; set; }
        [Required(ErrorMessage = "Komentarz jest wymagany.")]
        public string? Comment { get; set; }

        public int ApplicationId { get; set; }

        public string? UserEmail { get; set; } 

        public DateTime? OpinionDateAdd { get; set; }
    }
}
