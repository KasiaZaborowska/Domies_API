﻿using System;
using System.Collections.Generic;

namespace DomiesAPI.Models;

public partial class Application
{
    public int Id { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public int OfferId { get; set; }

    public string ToUser { get; set; } = null!;

    public DateTime? ApplicationDateAdd { get; set; }

    public virtual Offer Offer { get; set; } = null!;

    public virtual ICollection<Opinion> Opinions { get; set; } = new List<Opinion>();

    public virtual User ToUserNavigation { get; set; } = null!;
}
