using System.ComponentModel.DataAnnotations;
using WorshipProgramPlannerApp.Models;

namespace WorshipProgramPlannerApp.DTOs
{
    public class WorshipProgramDTO
    {
        public int WorshipProgramId { get; set; }

        [Required(ErrorMessage = "Performer name is required.")]
        public string PerformerName { get; set; }

        public string? PoetryName { get; set; }

        public string? SongName { get; set; }

        public string? Comment { get; set; }

        [Required(ErrorMessage = "Worship ID is required.")]
        public int WorshipId { get; set; }
    }
}
