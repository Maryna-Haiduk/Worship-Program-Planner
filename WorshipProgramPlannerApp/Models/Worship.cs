namespace WorshipProgramPlannerApp.Models
{
    public class Worship
    {
        public int WorshipId { get; set; }
        public DateTime WorshipDate { get; set; }
        public string? WorshipName { get; set; }
        public ICollection<WorshipProgram> WorshipPrograms { get; set; } = new List<WorshipProgram>();
    }
}
