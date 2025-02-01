using Microsoft.EntityFrameworkCore;
using WorshipProgramPlannerApp.Models;


namespace WorshipProgramPlannerApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Worship> Worships { get; set; }
        public DbSet<WorshipProgram> WorshipPrograms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
