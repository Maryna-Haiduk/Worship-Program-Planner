using WorshipProgramPlannerApp.Data;
using Microsoft.EntityFrameworkCore;
using WorshipProgramPlannerApp.Repositories;

namespace WorshipProgramPlannerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseLazyLoadingProxies());

            builder.Services.AddScoped<IWorshipRepository, WorshipRepository>();
            builder.Services.AddScoped<IWorshipProgramRepository, WorshipProgramRepository>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Worship}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
