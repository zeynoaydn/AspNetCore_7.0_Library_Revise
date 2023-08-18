using Microsoft.EntityFrameworkCore;
using Mvc_Project.Models;
using Mvc_Project.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Mvc_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();


            ////
            builder.Services.AddDbContext<ProjectDbContext>(options => 
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<ProjectDbContext>();
            builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ProjectDbContext>().AddDefaultTokenProviders();
            builder.Services.AddRazorPages();
            ///
            builder.Services.AddScoped<IKitapTuruRepository,KitapTuruRepository>();
            builder.Services.AddScoped<IKitapRepository, KitapRepository>();
            builder.Services.AddScoped<IKiralamaRepository, KiralamaRepository>();
            
            builder.Services.AddScoped<IEmailSender,EmailSender>();
            ///

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //
            app.MapRazorPages();
            //

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}