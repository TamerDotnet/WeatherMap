using WeatherMap.API.Models;

namespace WeatherMap.API.Services
{
    public interface IWeatherQueryService
    {
        WeatherMapResult SearchForWeather(SearchTerms searchTerms);
    }
}
