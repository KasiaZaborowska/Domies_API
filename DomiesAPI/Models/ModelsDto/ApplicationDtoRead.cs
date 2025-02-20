using System.Globalization;

namespace DomiesAPI.Models.ModelsDto
{
    public class ApplicationDtoRead
    {
        public int Id { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int OfferId { get; set; }

        public string Applicant { get; set; } = null!;

        public string ApplicationStatus { get; set; } = null!;
        public string? Note { get; set; }

        //public string PetName { get; set; }
        public List<AnimalDto>? Animals { get; set; }
        public List<OpinionDto>? Opinions { get; set; }

        public DateTime? ApplicationDateAdd { get; set; }


    }
}
