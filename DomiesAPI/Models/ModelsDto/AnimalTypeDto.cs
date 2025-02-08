using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomiesAPI.Models.ModelsDto
{
    public class AnimalTypeDto
    {
        public int AnimalTypeId { get; set; }
        [Required(ErrorMessage = "Typ zwierzęcia jest wymagany.")]
        public string Type { get; set; }

        //public virtual ICollection<OfferAnimalType> OfferAnimalTypes { get; set; } = new List<OfferAnimalType>();
    }
}
