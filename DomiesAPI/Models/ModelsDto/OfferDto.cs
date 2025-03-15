using System.ComponentModel.DataAnnotations;

namespace DomiesAPI.Models.ModelsDto
{
    public class OfferDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Imię opiekuna jest wymagane.")]
        public string? Name { get; set; } = null!;

        [Required(ErrorMessage = "Opis oferty jest wymagany.")]
        public string? OfferDescription { get; set; } = null!;

        [Required(ErrorMessage = "Opis opiekuna jest wymagany.")]
        public string? PetSitterDescription { get; set; } = null!;

        public string? Host { get; set; } = null!;

        public int? AddressId { get; set; }

        public DateTime? DateAdd { get; set; }

        [Required(ErrorMessage = "Państwo w adresie jest wymagane.")]
        public string? Country { get; set; } = null!;

        [Required(ErrorMessage = "Miasto w adresie jest wymagane.")]
        public string? City { get; set; } = null!;

        [Required(ErrorMessage = "Ulica w adresie jest wymagana.")]
        public string? Street { get; set; } = null!;

        [Required(ErrorMessage = "Kod pocztowy jest wymagany.")]
        public string? PostalCode { get; set; } = null!;

        [Required(ErrorMessage = "Cena oferty jest wymagana.")]
        public decimal? Price { get; set; }

        //public List<AnimalTypeDto>? OfferAnimalTypes { get; set; }

        [Required(ErrorMessage = "Typ zwierzęcia akceptowanego przez ofertę jest wymagany.")]
        [MinLength(1, ErrorMessage = "Wymagane jest zaznaczenie co najmniej 1 opcji.")]
        public List<string>? OfferAnimalTypes { get; set; } = null!;

        public List<int>? Facilities { get; set; }


        //[Required(ErrorMessage = "Zdjęcie jest wymagane.")]
        public IFormFile? File { get; set; }

        
        public string? Photo { get; set; }
    }
}
