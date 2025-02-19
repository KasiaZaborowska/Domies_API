using System.ComponentModel.DataAnnotations;

namespace DomiesAPI.Models.ModelsDto
{
    public class AnimalDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię zwierzaka jest wymagane.")]
        public string PetName { get; set; } = null!;

        [Required(ErrorMessage = "Szczegółowy opis zwierzaka jest wymagany.")]
        public string SpecificDescription { get; set; } = null!;

        public string? Owner { get; set; } = null!;

        [Required(ErrorMessage = "Typ zwierzaka jest wymagany.")]
        [MinLength(1, ErrorMessage = "Wymagane jest zaznaczenie 1 spośród dostępnych opcji.")]
        public int AnimalType { get; set; }
        public string? Type { get; set; }

    }
}
