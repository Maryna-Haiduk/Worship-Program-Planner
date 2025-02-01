namespace WorshipProgramPlannerApp.Models
{
    public class WorshipProgram
    {
        public int WorshipProgramId { get; set; }
        public string PerformerName { get; set; }
        public string? PoetryName { get; set; }
        public string? SongName { get; set; }
        public string? Comment { get; set; }

        // Foreign Key
        public int WorshipId { get; set; }
        public Worship Worship { get; set; }
    }
}
