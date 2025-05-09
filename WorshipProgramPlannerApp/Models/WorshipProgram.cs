﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace WorshipProgramPlannerApp.Models
{
    public class WorshipProgram
    {
        [Key]
        public int WorshipProgramId { get; set; }
        public string PerformerName { get; set; }
        public string? PoetryName { get; set; }
        public string? SongName { get; set; }
        public string? Comment { get; set; }

        // Foreign Key
        public int WorshipId { get; set; }

        [BindNever]
        public virtual Worship Worship { get; set; }
    }
}
