namespace WeatherMap.API.Models
{
    public class SearchTerms
    {
        public SearchTerms(string country, string city)
        {
            Country = country;
            City = city;
        }

        public string Country { get; set; }
        public string City { get; set; }
    }
}
