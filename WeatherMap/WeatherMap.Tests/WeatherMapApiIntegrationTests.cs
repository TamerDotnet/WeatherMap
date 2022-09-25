using FluentAssertions;
using NUnit.Framework;
using System.Net;
using WeatherMap.API.Attributes;
using WeatherMap.API.Controllers;
using WeatherMap.API.Settings;
using WeatherMap.Tests.Builders;

namespace WeatherMap.Tests
{
    [TestFixture(Category = "Integration")]
    public class WeatherMapApiIntegrationTests
    { 
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;


        [SetUp]
        public void Setup()
        {         
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }
        
        [Explicit]
        public async Task CallAPI_Return_OkCode_HasValidKey()
        {
            //arrange
            Random random = new Random();
            int idx = random.Next(0, ApiKeys.AvailableKeysToChooseFrom.Length);
            var validKey = ApiKeys.AvailableKeysToChooseFrom[idx];
            _client.DefaultRequestHeaders.Add("APIKey", validKey);

            //act
            var response = await _client.GetAsync("/api/WeatherForecast/weather/uk/london");

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Explicit]
        public async Task CallAPI_Return_UnauthorizedCode_HasNoKey()
        { 
            //arrange
             _client.DefaultRequestHeaders.Add("APIKey", "NOT-VALID-KEY");

            //act
             var response = await _client.GetAsync("/api/WeatherForecast/weather/uk/london");

            //assert
             response.StatusCode.Should().Be(HttpStatusCode.Unauthorized); 
        }
        [Explicit]
        public void WeatherForecastController_ShouldBe_Protected_With_API_Key_Required()
        {
            // arrange
            var controllerType = typeof(WeatherForecastController);
            var attributeType = typeof(ApiKeyRequiredAttribute);
            var controllerAttributes = controllerType.GetCustomAttributes(false);

            // act
            var isAttributeApplied = controllerAttributes.Any(x => x.GetType() == attributeType);

            // assert
            Assert.IsTrue(isAttributeApplied);
        } 
    }
}
