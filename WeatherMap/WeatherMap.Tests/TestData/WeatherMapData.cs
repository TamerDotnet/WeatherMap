namespace WeatherMap.Tests.TestData
{
    public  class WeatherMapData
    {
        public const string ResponseTestData = @"{
                ""coord"": {
                    ""lon"": -0.1257,
                    ""lat"": 51.5085
                },
                ""weather"": [
                    {
                        ""id"": 803,
                        ""main"": ""Clouds"",
                        ""description"": ""broken clouds"",
                        ""icon"": ""04n""
                    }
                ],
                ""base"": ""stations"",
                ""main"": {
                ""temp"": 284.09,
                    ""feels_like"": 283.56,
                    ""temp_min"": 281.18,
                    ""temp_max"": 286.62,
                    ""pressure"": 1021,
                    ""humidity"": 89
                },
                ""visibility"": 10000,
                ""wind"": {
                ""speed"": 1.35,
                    ""deg"": 189,
                    ""gust"": 3.11
                },
                ""clouds"": {
                ""all"": 70
                },
                ""dt"": 1663815849,
                ""sys"": {
                ""type"": 2,
                    ""id"": 2075535,
                    ""country"": ""GB"",
                    ""sunrise"": 1663825577,
                    ""sunset"": 1663869622
                },
                ""timezone"": 3600,
                ""id"": 2643743,
                ""name"": ""London"",
                ""cod"": 200
            }";
    }
}
