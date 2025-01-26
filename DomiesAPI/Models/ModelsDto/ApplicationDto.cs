﻿namespace DomiesAPI.Models.ModelsDto
{
    public class ApplicationDto
    {
        public int Id { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int OfferId { get; set; }

        //public string ToUser { get; set; } = null!;
        public string? Note { get; set; }

        public List<string>? Animals { get; set; }

        //public List<string> Animals { get; set; } = new List<string>();

        //public List<int> AnimalsINT => Animals.Select(a => int.Parse(a)).ToList();

        public DateTime? ApplicationDateAdd { get; set; }


    }
}
