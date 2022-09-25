using WeatherMap.API.Models;

namespace WeatherMap.API.Services
{
    public interface IWeatherQueryService
    {
        Task<WeatherMapResult?> SearchForWeatherAsync(SearchTerms searchTerms);
    }
}
