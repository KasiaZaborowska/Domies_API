using System.Globalization;

namespace DomiesAPI.Models.ModelsDto
{
    public class ApplicationDtoRead
    {
        public int Id { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int OfferId { get; set; }

        public string ToUser { get; set; } = null!;

        //public string PetName { get; set; }
        public List<AnimalDto>? Animals { get; set; }

        public DateTime? ApplicationDateAdd { get; set; }


    }
}
