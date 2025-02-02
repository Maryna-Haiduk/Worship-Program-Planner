using System.ComponentModel.DataAnnotations;

namespace WorshipProgramPlannerApp.Models
{
    public class Worship
    {
        [Key]
        public int WorshipId { get; set; }

        [Required(ErrorMessage = "Worship Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMM dd}", ApplyFormatInEditMode = true)]
        public DateTime WorshipDate { get; set; } = DateTime.Now;
        public string? WorshipName { get; set; }
        public ICollection<WorshipProgram> WorshipPrograms { get; set; } = new List<WorshipProgram>();
    }
}
