namespace DomiesAPI.Models.ModelsDto
{
    public class AnimalDto
    {
        public int Id { get; set; }

        public string PetName { get; set; } = null!;

        public string SpecificDescription { get; set; } = null!;

        public string? Owner { get; set; } = null!;

        public int AnimalType { get; set; }
        public string? Type { get; set; }

    }
}
