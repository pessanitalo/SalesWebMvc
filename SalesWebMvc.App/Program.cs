using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data.Context;
using SalesWebMvc.App.Data;
using SalesWebMvc.App.Services;

namespace SalesWebMvc.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SalesWebMvcAppContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SalesWebMvcAppContext") ?? throw new InvalidOperationException("Connection string 'SalesWebMvcAppContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DataContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<SellerService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}