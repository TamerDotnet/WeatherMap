using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using WeatherMap.API.Models;
using WeatherMap.Tests.TestData;

namespace WeatherMap.UnitTests
{
    [TestFixture(Category = "unit")]
    public class OpenWeatherResponseUnitTests
    {
        [Test]
        public void WeatherMapResponse_Deserialize_Correct_DataFormat()
        {
            var weatherData = JsonConvert.DeserializeObject<WeatherMapResponse>(WeatherMapData.ResponseTestData);
            var description = weatherData.Weather.First().Description;
            description.Should().BeNull();
            description.Should().Be("broken clouds");
        }
    }
}