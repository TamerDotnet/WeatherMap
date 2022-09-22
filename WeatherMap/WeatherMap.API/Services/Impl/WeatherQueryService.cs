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

        

        public WeatherMapResult SearchForWeather(SearchTerms searchTerms)
        {
            var validationErrors = _weatherQueryValidator.ValidateSearchTerms(searchTerms);
            if (validationErrors.Any())
                return new FailedWeatherMapResult(validationErrors.ToArray());

            var response = _openWeatherMapHttp.GetOpenWeatherMap(searchTerms);
            if(response.Result == null)
                return new FailedWeatherMapResult(new[] {"cannot get result"});

            return new SuccessfullWeatherMapResult(response.Result);

        }

       

        
    }
}
