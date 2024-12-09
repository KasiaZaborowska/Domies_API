using System;
using System.Collections.Generic;

namespace DomiesAPI.Models;

public partial class AnimalType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
}
