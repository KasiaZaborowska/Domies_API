﻿namespace DomiesAPI.Models.ModelsDto
{
    public class OfferDto
    {
        public int Id { get; set; }

        public string? Title { get; set; } = null!;

        public string? Description { get; set; } = null!;

        public string? Host { get; set; } = null!;

        public int? AddressId { get; set; }

        public DateTime? DateAdd { get; set; }
        
        public string? Country { get; set; } = null!;

        public string? City { get; set; } = null!;

        public string? Street { get; set; } = null!;

        public string? PostalCode { get; set; } = null!;

        //public List<AnimalTypeDto>? OfferAnimalTypes { get; set; }
        public string OfferAnimalTypes { get; set; } = null!;

        public IFormFile? File { get; set; }
        public string? Photo { get;  set; }
    }
}
