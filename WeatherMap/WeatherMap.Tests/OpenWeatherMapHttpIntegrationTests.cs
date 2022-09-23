using FluentAssertions;
using NUnit.Framework;
using WeatherMap.API.Core;
using WeatherMap.API.Models;

namespace WeatherMap.Tests
{
    [TestFixture(Category = "Integration")]
    public class OpenWeatherMapHttpIntegrationTests
    {
        [Test]
        public async Task Handle_OpenWeatherMapHttp_MakeExternalCall_GetOpenWeatherMapAsync_FoundMatching()
        {
            //arrange
            var searchTerms = new SearchTerms("uk", "London"); 
            var openWeatherMapHttp = new OpenWeatherMapHttp(new HttpClient());

            //act
            var weatherMapResult = await openWeatherMapHttp.GetOpenWeatherMapAsync(searchTerms);

            //assert
            weatherMapResult.Should().NotBe(null);
            Assert.That(weatherMapResult, Is.InstanceOf<SuccessfullWeatherMapResult>());

            var result = weatherMapResult as SuccessfullWeatherMapResult;
            result.WeatherMapResponse.Weather.ToArray()[0].Description.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Handle_OpenWeatherMapHttp_MakeExternalCall_GetOpenWeatherMapAsync_NoMatching()
        {
            //arrange
            var searchTerms = new SearchTerms("oreiie", "ueruuen");
            var openWeatherMapHttp = new OpenWeatherMapHttp(new HttpClient());

            //act
            var weatherMapResponse = await openWeatherMapHttp.GetOpenWeatherMapAsync(searchTerms);

            //assert
            Assert.That(weatherMapResponse, Is.InstanceOf<FailedWeatherMapResult>());
            var result = weatherMapResponse as FailedWeatherMapResult;

            result.Errors.Length.Should().Be(1);
            result.Errors.ToArray()[0].Should().Be("Not Found");


        }
    }
}
