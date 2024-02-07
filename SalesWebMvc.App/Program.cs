using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data.Context;
using SalesWebMvc.App.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace SalesWebMvc.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DataContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<SellerService>();
            builder.Services.AddScoped<DepartmentService>();
            builder.Services.AddScoped<SalesRecordService>();
            

            var app = builder.Build();

            var ptbr = new CultureInfo("pt-br");
            var localization = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(ptbr),
                SupportedCultures = new List<CultureInfo> { ptbr },
                SupportedUICultures = new List<CultureInfo> { ptbr },
            };

            if (!app.Environment.IsDevelopment())
            {
                app.UseRequestLocalization(localization);
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