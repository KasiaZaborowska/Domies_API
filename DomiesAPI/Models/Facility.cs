using System;
using System.Collections.Generic;

namespace DomiesAPI.Models;

public partial class Facility
{
    public int Id { get; set; }

    public string FacilitiesType { get; set; } = null!;

    public string FacilitiesDescription { get; set; } = null!;
}
