using WeatherMap.API.Models;

namespace WeatherMap.API.Services.Impl
{
    public class WeatherQueryValidator : IWeatherQueryValidator
    { 
        public IEnumerable<string> ValidateSearchTerms(SearchTerms searchTerms)
        {
            if (string.IsNullOrWhiteSpace(searchTerms.Country))
                yield return "Country is missing";


            if (string.IsNullOrWhiteSpace(searchTerms.City))
                yield return "City is missing";
        }
    }
}
