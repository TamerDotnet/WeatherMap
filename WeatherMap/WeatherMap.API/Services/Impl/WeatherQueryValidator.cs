using WeatherMap.API.Models;

namespace WeatherMap.API.Services.Impl
{
    public class WeatherQueryValidator : IWeatherQueryValidator
    { 
        public IEnumerable<string> ValidateSearchTerms(SearchTerms searchTerms)
        {
            if (string.IsNullOrEmpty(searchTerms.Country) ||
                string.IsNullOrWhiteSpace(searchTerms.Country))
                yield return "Country is missing";


            if (string.IsNullOrEmpty(searchTerms.City)||
                 string.IsNullOrWhiteSpace(searchTerms.City))
                yield return "City is missing";
        }
    }
}
