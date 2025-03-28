﻿namespace DomiesAPI.Models.ModelsDto
{
    public class OfferDtoRead
    {
        public int Id { get; set; }

        public string? Name { get; set; } = null!;

        public string? OfferDescription { get; set; } = null!;
        public string? PetSitterDescription { get; set; } = null!;

        public string? Host { get; set; } = null!;
        public string? HostPhoneNumber { get; set; } = null!;

        public int? AddressId { get; set; }

        public DateTime? DateAdd { get; set; }

        public string? Country { get; set; } = null!;

        public string? City { get; set; } = null!;

        public string? Street { get; set; } = null!;

        public string? PostalCode { get; set; } = null!;
        public decimal? Price { get; set; }
        //public List<OpinionDto>? OpinionsList { get; set; }

        //public List<AnimalTypeDto>? OfferAnimalTypes { get; set; }
        public string OfferAnimalTypes { get; set; } = null!;
        public List<ApplicationDtoRead> Applications { get; set; } = null!;
        public List<FacilityDto>? Facilities { get; set; }

        public IFormFile? File { get; set; }
        public string? Photo { get; set; }
    }
}
