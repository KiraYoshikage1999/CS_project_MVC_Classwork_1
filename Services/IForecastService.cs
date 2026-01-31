using CS_project_MVC_Classwork_1B.DTO;

namespace CS_project_MVC_Classwork_1B.Services
{
    public interface IForecastService
    {
        Task<ForecastDto> GetForecastAsync(double lat, double lon);
    }
}
