global using Microsoft.AspNetCore.Components;
global using Microsoft.AspNetCore.Components.Authorization;
global using Microsoft.AspNetCore.Components.Web;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.UI;
global using Microsoft.EntityFrameworkCore;
global using PpsPlansApp.Areas.Identity;
global using PpsPlansApp.Data;
global using Radzen;
global using PpsPlansApp.Services;

namespace PpsPlansApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>(options =>
               {
                   options.UseInMemoryDatabase("dev");
                   options.EnableDetailedErrors(true);
                   options.EnableSensitiveDataLogging();

                   //options.UseSqlServer(connectionString);
               });

            services.AddDbContextFactory<ApplicationDbContext>(lifetime: ServiceLifetime.Scoped);
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor(options =>
            {
                options.DetailedErrors = true;
            });

            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddSingleton<ContractService>();


            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}