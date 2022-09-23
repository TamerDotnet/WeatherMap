using WeatherMap.API.Core;
using WeatherMap.API.Models;

namespace WeatherMap.API.Services.Impl
{
    public class WeatherQueryService : IWeatherQueryService
    {
        private readonly IOpenWeatherMapHttp _openWeatherMapHttp;
        private readonly IWeatherQueryValidator _weatherQueryValidator;

        public WeatherQueryService(IOpenWeatherMapHttp openWeatherMapHttp, 
                IWeatherQueryValidator weatherQueryValidator) 
        {
            _openWeatherMapHttp = openWeatherMapHttp;
            _weatherQueryValidator = weatherQueryValidator;
        }

        

        public async Task<WeatherMapResult> SearchForWeatherAsync(SearchTerms searchTerms)
        {
            var validationErrors = _weatherQueryValidator.ValidateSearchTerms(searchTerms);
            if (validationErrors.Any())
                 return   new FailedWeatherMapResult(validationErrors.ToArray());

            var weatherMapResult = await _openWeatherMapHttp.GetOpenWeatherMapAsync(searchTerms);

            return weatherMapResult;

        }
    }
}
