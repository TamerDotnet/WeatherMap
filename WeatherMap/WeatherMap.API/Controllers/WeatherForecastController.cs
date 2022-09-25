using Microsoft.AspNetCore.Mvc;
using WeatherMap.API.Attributes;
using WeatherMap.API.Models;
using WeatherMap.API.Services;

namespace WeatherMap.API.Controllers
{

    [ApiController]
    [ApiKeyRequired]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherQueryService _weatherQueryService;

        public WeatherForecastController(IWeatherQueryService weatherQueryService)
        {
            _weatherQueryService = weatherQueryService;
        }

        [HttpGet("weather/{country}/{city}", Name = "GetWeatherByCountryCity")]
        public async Task<string> Get(string country, string city)
        {
            var searchTerms = new SearchTerms(country, city);
            var result = await _weatherQueryService.SearchForWeatherAsync(searchTerms);
 
            if (result is FailedWeatherMapResult)
                return GetFailedResultData(result);

            return GetSuccessfullResultData(result);


        }

        private string GetSuccessfullResultData(WeatherMapResult result)
        {
            var weather = ((SuccessfullWeatherMapResult)result)
                       .WeatherMapResponse.Weather;
            return weather.FirstOrDefault()?.Description?? "Weather not found";
        }

        private string GetFailedResultData(WeatherMapResult result)
        {
            var errors = ((FailedWeatherMapResult)result).Errors;
            return string.Join(",", errors);
        }
    }
}