using CS_project_MVC_Classwork_1B.DTO;

namespace CS_project_MVC_Classwork_1B.Services
{
    public class GeoService : IGeoService
    {
        private readonly HttpClient _httpClient;

        public GeoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GeoResultDto?> FindCityAsync(string city)
        {
            var safeCity = Uri.EscapeDataString(city.Trim());

            var url =
                $"search" +
                $"?name={safeCity}" +
                $"&count=1" +
                $"&language=en" +
                $"&format=json";

            var data = await _httpClient.GetFromJsonAsync<GeoResponseDto>(url);

            return data?.Results?.FirstOrDefault();
        }

    }
}
