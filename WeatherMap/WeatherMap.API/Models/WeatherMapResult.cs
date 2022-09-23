using Microsoft.AspNetCore.Mvc;

namespace WeatherMap.API.Models
{
    public abstract class WeatherMapResult
    {
         
    }
    public class SuccessfullWeatherMapResult: WeatherMapResult
    {
        public SuccessfullWeatherMapResult(WeatherMapResponse weatherMapResponse)
        {
            WeatherMapResponse = weatherMapResponse;
        }

        public WeatherMapResponse WeatherMapResponse { get; set; }
    }
    public class FailedWeatherMapResult : WeatherMapResult
    {
        public FailedWeatherMapResult(string[] errors)
        {
            Errors = errors ?? throw new ArgumentException("Errors not provided");
        }

        public string[] Errors { get; set; }
    }

}
