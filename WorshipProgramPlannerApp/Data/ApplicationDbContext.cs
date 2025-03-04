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
            // base.OnModelCreating(modelBuilder);

            // Fluent API configurations(if needed)
            modelBuilder.Entity<Worship>()
                    .HasMany(w => w.WorshipPrograms)
                    .WithOne(wp => wp.Worship)
                    .HasForeignKey(wp => wp.WorshipId)
                    .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Worship>()
                    .Property(w => w.WorshipId)
                    .ValueGeneratedOnAdd(); // Ensures auto-incrementing behavior
            
            modelBuilder.Entity<WorshipProgram>()
                    .Property(w => w.WorshipProgramId)
                    .ValueGeneratedOnAdd(); // Ensures auto-incrementing behavior

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies(); // Enable Lazy Loading
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies(); // Enable Lazy Loading
            }
        }
    }
}
