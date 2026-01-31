using CS_project_MVC_Classwork_1B.Services;
using Microsoft.AspNetCore.Mvc;

namespace CS_project_MVC_Classwork_1B.Controllers
{
    public class WeatherController : Controller
    {

        public readonly IGeoService _geoService;
        public readonly IForecastService _forecastService;

        public WeatherController(IForecastService forecastService, IGeoService geoService)
        {
            _geoService = geoService;
            _forecastService = forecastService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string city)
        {
            if (city == null)
            {
                ViewBag.Error = "City is Empty";
                return View();
            }
            var place = await _geoService.FindCityAsync(city);
            if (place == null)
            {
                ViewBag.Error = "Place is empty";
                return View();
            }

            var data = await _forecastService.GetForecastAsync(place.Latitude, place.Longitude);
            if (data == null)
            {
                ViewBag.Error = "Cannot take info";
                return View();
            }

            ViewBag.Place = $"{place.Name}";
            return View(data);
        }
    }
}
