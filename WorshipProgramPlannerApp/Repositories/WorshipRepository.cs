using WorshipProgramPlannerApp.Models;
using WorshipProgramPlannerApp.Data;

namespace WorshipProgramPlannerApp.Repositories
{
    public class WorshipRepository : Repository<Worship>, IWorshipRepository
    {
        public WorshipRepository(ApplicationDbContext context) : base(context) { }
    }
}
