using AttendenceSystem.IRepo;
using AttendenceSystem.Repo;

using AttendenceSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AttendenceSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<InstructorIRepo, InstructorRepo>();
            builder.Services.AddTransient<IEmpRepo, EmpRepo>();
            builder.Services.AddTransient<TrackIRepo, TrackRepo>();


            builder.Services.AddTransient<IAccountRepo, AccountRepo>();
            builder.Services.AddTransient<IStudentRepo, StudentRepo>();
            builder.Services.AddTransient<IStudentService, StudentService>();
            builder.Services.AddTransient<IUserRepo, UserRepo>();
            builder.Services.AddTransient<INotificationService, NotificationService>();


            builder.Services.AddSession();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
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

                pattern: "{controller=Account}/{action=login}");


            app.Run();
        }
    }
}