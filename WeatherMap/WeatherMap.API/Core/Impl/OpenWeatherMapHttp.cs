using Newtonsoft.Json;
using WeatherMap.API.Models;

namespace WeatherMap.API.Core
{
    public class OpenWeatherMapHttp : IOpenWeatherMapHttp
    {
        private readonly HttpClient _httpClient;
        private readonly string baseUrl = "http://api.openweathermap.org";
        private readonly string apiKey = "8b7535b42fe1c551f18028f64e8688f7";

        public OpenWeatherMapHttp(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<WeatherMapResult?> GetOpenWeatherMapAsync(SearchTerms searchTerms)
        {
            var requestUri = $"/data/2.5/weather?q={searchTerms.Country},{searchTerms.City}&appid={apiKey}";
            var response = await _httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var stringData = await response.Content.ReadAsStringAsync();
                var weatherMapResponse = JsonConvert.DeserializeObject<WeatherMapResponse>(stringData);

                if (weatherMapResponse == null)
                    return new FailedWeatherMapResult(new[] { "Weather Could not be found" });

                return new SuccessfullWeatherMapResult(weatherMapResponse);

            }
            else
            {
                return new FailedWeatherMapResult(new[] { response.ReasonPhrase ?? response.StatusCode.ToString() });
            }
        }
    }
}
