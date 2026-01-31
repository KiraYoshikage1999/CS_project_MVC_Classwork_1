using CS_project_MVC_Classwork_1B.DTO;

namespace CS_project_MVC_Classwork_1B.Services
{
    public class ForecastService
        //: IForecastService
    {
        private readonly HttpClient _httpClient;
        public ForecastService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //public Task<ForecastDto> GetForecastAsync(double lat, double lon)
        //{
        //    var url = $"forecast?latitude={lat}&longtude={lon}&hourly=temperature_2m&timezone=auto";
             

        //}
    }
}
