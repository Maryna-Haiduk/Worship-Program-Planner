using WorshipProgramPlannerApp.Data;
using Microsoft.EntityFrameworkCore;
using WorshipProgramPlannerApp.Repositories;
using Microsoft.AspNetCore.Localization;
using System.Globalization;


namespace WorshipProgramPlannerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Localization
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Register MVC + Localization
            builder.Services.AddControllersWithViews()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            // Add EF and repositories
            builder.Services.AddDbContext<ApplicationDbContext>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseLazyLoadingProxies());

            builder.Services.AddScoped<IWorshipRepository, WorshipRepository>();
            builder.Services.AddScoped<IWorshipProgramRepository, WorshipProgramRepository>();

            var app = builder.Build();

            // Configure Supported Cultures
            var supportedCultures = new[] { "en", "ru", "uk" };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture("en")
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Worship}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
