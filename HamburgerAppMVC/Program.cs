using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HamburgerAppMVC.Areas.Identity.Data;
namespace HamburgerAppMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Ortam ad�n� almak ve ilgili JSON dosyas�n� y�klemek i�in
            var env = builder.Environment.EnvironmentName;

            // Ortam ad�n� konsola yazd�rmak
            Console.WriteLine($"Running in environment: {env}");

            //// Genel ayar dosyas�n� ve ortam �zel ayar dosyas�n� y�klemek
            //builder.Configuration
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);

            Console.WriteLine(builder.Configuration.GetConnectionString("AppDbConStr"));

































            var connectionString = builder.Configuration.GetConnectionString("AppDbConStr") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));



            builder.Services.AddSingleton<MailSenderConfirm>();//mail sender kullan�m� i�in 



            //identity
            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();//Rolleri de ekledik




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

            //Selam :)

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();//identity

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
