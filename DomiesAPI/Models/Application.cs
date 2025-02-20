using System;
using System.Collections.Generic;

namespace DomiesAPI.Models;

public partial class Application
{
    public int Id { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }

    public int OfferId { get; set; }

    public string Applicant { get; set; } = null!;
    public string ApplicationStatus { get; set; } = null!;

    public DateTime? ApplicationDateAdd { get; set; }
    public string Note { get; set; }

    public virtual Offer Offer { get; set; } = null!;

    public virtual ICollection<Opinion> Opinions { get; set; } = new List<Opinion>();

    public virtual User ApplicantNavigation { get; set; } = null!;
    public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
    //public virtual ICollection<ApplicationsAnimal> ApplicationsAnimals { get; set; } = new List<ApplicationsAnimals>();
}
