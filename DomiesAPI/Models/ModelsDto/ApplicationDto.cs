using System.ComponentModel.DataAnnotations;

namespace DomiesAPI.Models.ModelsDto
{
    public class ApplicationDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Data początkowa jest wymagana.")]
        public DateTime DateStart { get; set; }

        [Required(ErrorMessage = "Data kopńcowa jest wymagana.")]
        public DateTime DateEnd { get; set; }

        public int OfferId { get; set; }

        //public string ToUser { get; set; } = null!;
        public string? Note { get; set; }

        [MinLength(1, ErrorMessage = "Wymagane jest zaznaczenie co najmniej 1 opcji spośród dostępnych zwierząt.")]
        [Required(ErrorMessage = "Wybór zwierząt jest wymagany.")]
        public List<int>? Animals { get; set; }

        //public List<string> Animals { get; set; } = new List<string>();

        //public List<int> AnimalsINT => Animals.Select(a => int.Parse(a)).ToList();

        public DateTime? ApplicationDateAdd { get; set; }


    }
}
