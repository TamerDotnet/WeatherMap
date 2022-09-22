using WeatherMap.API.Models;

namespace WeatherMap.API.Core
{
    public interface IOpenWeatherMapHttp
    {
        Task<WeatherMapResponse?> GetOpenWeatherMap(SearchTerms searchTerms);
    }
}
