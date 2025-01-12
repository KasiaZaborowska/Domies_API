using System;
using System.Collections.Generic;

namespace DomiesAPI.Models;

public partial class Animal
{
    public int Id { get; set; }

    public string PetName { get; set; } = null!;

    public string SpecificDescription { get; set; } = null!;

    public string Owner { get; set; } = null!;

    public int AnimalType { get; set; }

    public virtual AnimalType AnimalTypeNavigation { get; set; } = null!;

    public virtual User OwnerNavigation { get; set; } = null!;

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}
