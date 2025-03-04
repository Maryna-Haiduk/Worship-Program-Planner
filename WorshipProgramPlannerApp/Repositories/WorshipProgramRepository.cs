using WorshipProgramPlannerApp.Models;
using WorshipProgramPlannerApp.Data;

namespace WorshipProgramPlannerApp.Repositories
{
    public class WorshipProgramRepository : Repository<WorshipProgram>, IWorshipProgramRepository
    {
        public WorshipProgramRepository(ApplicationDbContext context) : base(context) { }
    }
}