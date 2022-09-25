using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;

namespace WeatherMap.API.Configurations
{
    public class WeatherMapRateLimitConfiguration : RateLimitConfiguration
    {
        public WeatherMapRateLimitConfiguration(IOptions<IpRateLimitOptions> ipOptions,
                                                IOptions<ClientRateLimitOptions> clientOptions): base(ipOptions, clientOptions)
        {
        }

        public override void RegisterResolvers()
        {
            ClientResolvers.Add(new QueryStringClientIdResolveContributor());
        } 
    }
    public class QueryStringClientIdResolveContributor : IClientResolveContributor
    {
        public Task<string> ResolveClientAsync(HttpContext httpContext)
        {
            var headers = httpContext.Request.Headers;

            if (headers.ContainsKey("APIKey")
            && !string.IsNullOrWhiteSpace(headers["APIKey"]))
            {
                return Task.FromResult<string>(headers["APIKey"]);
            } 
            return Task.FromResult(string.Empty);
        }
    }
}
