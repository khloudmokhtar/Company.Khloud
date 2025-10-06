using Company.Khloud.BLL;
using Company.Khloud.BLL.Interfaces;
using Company.Khloud.BLL.Repositories;
using Company.Khloud.DAL.Data.Contexts;
using Company.Khloud.DAL.Models;
using Company.Khloud.PL.Mapping;
using Company.Khloud.PL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Company.Khloud.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(); //Register Buit-in MVC Services
                                                        // builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>(); //Allow DI For DepartmentRepository
                                                        //  builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); //Allow DI For EmployeeRepository
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                //options.UseSqlServer(builder.Confi guration["DefaultConnection"]);

            }); // Allow DI For CompanyDbContext

            // builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<CompanyDbContext>()
                            .AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(config =>
            {

                config.LoginPath = "/Account/SignIn";
            });
          

            //LifeTime

            //builder.Services.AddScoped(); // Allow through it depedience injection for specific Service / Create Object Life  Time  per Request - Unreaceable
            //builder.Services.AddTransient(); // Allow through it depedience injection for specific Service / Create Object Life  Time  per Operation 
            //builder.Services.AddSingleton(); // Allow through it depedience injection for specific Service /Create Object Life  Time  per Application

            builder.Services.AddScoped<IScopedService, ScopedService>(); //Per Request 
            builder.Services.AddTransient<ITransientService, TransientService>(); //Per Operation 
            builder.Services.AddSingleton<ISinglteonService, SingletonService>(); //Per Application



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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
