//using CS_project_MVC_Classwork_1B.MiddleWare;
using CS_project_MVC_Classwork_1B.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Csharp_modelContoroling_lesson
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Registration services .
            // Ensure the configured base URIs end with a trailing slash so relative paths are combined correctly
            // Коротко, я попросил агента посмотреть весь код и исправить его, он добавил ? к методам forecast и GeoService, а также перелопатил 
            // Програм файл, где поигрался со строкой, до сих пор понять не могу, реально ли была проблема в том, что он не правильно ставил СЛЕШ, 
            // Предполагаю, что из-за разности версий была в этом какая-то ошибка.
            var geoBase = builder.Configuration["OpenMeteoGeo:BaseUrl"] ?? string.Empty;
            if (!geoBase.EndsWith('/')) geoBase = geoBase.TrimEnd() + '/';

            var forecastBase = builder.Configuration["OpenMeteo:BaseUrl"] ?? string.Empty;
            if (!forecastBase.EndsWith('/')) forecastBase = forecastBase.TrimEnd() + '/';

            builder.Services.AddHttpClient<IGeoService, GeoService>(client => {
                client.BaseAddress = new Uri(geoBase);
            });


            builder.Services.AddHttpClient<IForecastService, ForecastService>(client => {
                client.BaseAddress = new Uri(forecastBase);
            });



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

            //app.UseMiddleware<RequestLoggingMiddleWare>()   ;

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
