using WeatherMap.API.Models;

namespace WeatherMap.API.Core
{
    public interface IOpenWeatherMapHttp
    {
        Task<WeatherMapResult?> GetOpenWeatherMapAsync(SearchTerms searchTerms);
    }
}
