using WeatherMap.API.Models;

namespace WeatherMap.API.Services
{
    public interface IWeatherQueryValidator
    {
        IEnumerable<string> ValidateSearchTerms(SearchTerms searchTerms);

    }
}
