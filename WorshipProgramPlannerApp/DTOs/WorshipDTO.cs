namespace WorshipProgramPlannerApp.DTOs
{
    public class WorshipDTO
    {
        public int WorshipId { get; set; }
        public string WorshipName { get; set; }
        public DateTime WorshipDate { get; set; }
        public List<WorshipProgramDTO> WorshipPrograms { get; set; } = new List<WorshipProgramDTO>();
    }

}
