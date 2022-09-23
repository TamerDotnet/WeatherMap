using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using WeatherMap.API.Core;
using WeatherMap.API.Models;
using WeatherMap.API.Services.Impl;
using WeatherMap.Tests.TestData;

namespace WeatherMap.Tests
{
    [TestFixture(Category = "Integration")]
    public class WeatherQueryServiceIntegrationTests
    {
        [TestCase("au", "", "City is missing")]
        [TestCase("au", "  ", "City is missing")]
        [TestCase("au", null, "City is missing")]
        [TestCase("", "mel", "Country is missing")]
        [TestCase("  ", "mel", "Country is missing")]
        [TestCase(null, "mel", "Country is missing")]
        public void Handle_SearchForWeather_Missing_OneDataEntry(string country, string city, string expectedValue)
        {
            //arrange
            var searchTerms = new SearchTerms(country, city);
            var weatherQueryService = SubstituteWeatherQueryService();


            //act
            var searchResult = weatherQueryService.SearchForWeatherAsync(searchTerms);

            //assert
            Assert.That(searchResult, Is.InstanceOf<FailedWeatherMapResult>());

            var failedWeatherMapResult = searchResult.Result as FailedWeatherMapResult;
            failedWeatherMapResult.Errors.Count().Should().Be(1);
            failedWeatherMapResult.Errors.ToArray()[0].Should().Be(expectedValue);
        }

        [TestCase("   ", "", "Country is missing,City is missing")]
        [TestCase("", null, "Country is missing,City is missing")]
        public void Handle_SearchForWeather_Missing_AllDataEntry(string country, string city, string expectedValue)
        {
            //arrange
            var searchTerms = new SearchTerms(country, city);
            var weatherQueryService = SubstituteWeatherQueryService();


            //act
            var searchResult = weatherQueryService.SearchForWeatherAsync(searchTerms);

            //assert
            Assert.That(searchResult, Is.InstanceOf<FailedWeatherMapResult>());

            var failedWeatherMapResult = searchResult.Result as FailedWeatherMapResult;
            failedWeatherMapResult.Errors.Count().Should().Be(2);
            string.Join(",", failedWeatherMapResult.Errors).Should().Be(expectedValue);
        }
        [Test]
        public void Handle_SearchForWeather_ReturnExpectedResult()
        {
            //arrange
            var searchTerms = new SearchTerms("uk", "London");
            var expectedWeatherData = GetSuccessfullWeatherMapResult();

            var weatherQueryValidator = Substitute.For<WeatherQueryValidator>();
            var openWeatherMapHttp = Substitute.For<IOpenWeatherMapHttp>();
            openWeatherMapHttp.GetOpenWeatherMapAsync(searchTerms).Returns(expectedWeatherData);

            var weatherQueryService = new WeatherQueryService(openWeatherMapHttp, weatherQueryValidator);

            //act
            var searchResult = weatherQueryService.SearchForWeatherAsync(searchTerms);

            //assert
            Assert.That(searchResult.Result, Is.InstanceOf<SuccessfullWeatherMapResult>());

            var result = searchResult.Result as SuccessfullWeatherMapResult;
            result.WeatherMapResponse.Weather.Should().BeEquivalentTo(expectedWeatherData.WeatherMapResponse.Weather);
        }


        private WeatherQueryService SubstituteWeatherQueryService()
        {
            var weatherQueryValidator = Substitute.For<WeatherQueryValidator>();
            var openWeatherMapHttp = Substitute.For<IOpenWeatherMapHttp>();

            return new WeatherQueryService(openWeatherMapHttp, weatherQueryValidator);
        }

        private SuccessfullWeatherMapResult GetSuccessfullWeatherMapResult()
        {
            var weatherMapResponse = JsonConvert.DeserializeObject<WeatherMapResponse>(WeatherMapData.ResponseTestData);
            return new SuccessfullWeatherMapResult(weatherMapResponse); 
        }
    }
}
