using WeatherMap.API.Models;

namespace WeatherMap.API.Core
{
    public class OpenWeatherMapHttp : IOpenWeatherMapHttp
    {
        public Task<WeatherMapResponse?> GetOpenWeatherMap(SearchTerms searchTerms)
        {
            throw new NotImplementedException();
        }
    }
}
