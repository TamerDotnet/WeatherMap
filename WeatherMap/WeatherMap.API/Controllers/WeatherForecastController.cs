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
        public async Task<WeatherMapResult?> Get(string country, string city)
        {
            var searchTerms = new SearchTerms(country, city);
            var result = await _weatherQueryService.SearchForWeatherAsync(searchTerms);
            return result;
        }
    }
}