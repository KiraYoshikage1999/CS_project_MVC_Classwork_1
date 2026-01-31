using CS_project_MVC_Classwork_1B.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Csharp_modelContoroling_lesson
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            //Registration services .

            builder.Services.AddHttpClient<IGeoService, GeoService>(client => {
                client.BaseAddress = new Uri(builder.Configuration["OpenMeteoGeo:BaseUrl"]!);
            });

            //builder.Services.AddHttpClient<IForecastService, ForecastService>(client =>
            //{
            //    client.BaseAddress = new Uri(builder.Configuration["OpenMeteo:BaseUrl"]!);
            //});

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
