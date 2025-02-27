using System;
using System.Collections.Generic;


namespace DomiesAPI.Models
{
    public class ApplicationsAnimals
    {
        public int? ApplicationId { get; set; }

        public int? AnimalId { get; set; }

        public virtual Application? Application { get; set; }

        public virtual Animal? Animal { get; set; }
    }
}