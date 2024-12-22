using System;
using System.Collections.Generic;

namespace DomiesAPI.Models;

public partial class Opinion
{
    public int Id { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public int ApplicationId { get; set; }

    public string UserEmail { get; set; } = null!;

    public DateTime? ApplicationDateAdd { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual User UserEmailNavigation { get; set; } = null!;
}
