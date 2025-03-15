namespace DomiesAPI.Models.ModelsDto
{
    public class NotificationDto
    {
        public string PetsitterEmail { get; set; } = null!;

        public List<string> PetName { get; set;} = null!;

        public int OfferId { get; set; }
    }
}
