using AspNetCoreRateLimit;
using WeatherMap.API.Configurations;
using WeatherMap.API.Core;
using WeatherMap.API.Services;
using WeatherMap.API.Services.Impl;

namespace WeatherMap.API
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddMemoryCache();
            services.AddSingleton<IRateLimitConfiguration, WeatherMapRateLimitConfiguration>();
            services.AddTransient<IWeatherQueryService, WeatherQueryService>();
            services.AddTransient<IOpenWeatherMapHttp, OpenWeatherMapHttp>();
            services.AddTransient<IWeatherQueryValidator, WeatherQueryValidator>();

            services.AddHttpClient<OpenWeatherMapHttp>();

            services.AddInMemoryRateLimiting();
            services.Configure<ClientRateLimitOptions>(options =>
            {
                options.EnableEndpointRateLimiting = true;
                options.StackBlockedRequests = false;
                options.HttpStatusCode = 429;
                options.ClientIdHeader = "X-ClientId";
                options.GeneralRules = new List<RateLimitRule>
                {
                    new RateLimitRule
                    {
                        Endpoint = "GET:/api/WeatherForecast/*",
                        Period = "1h",
                        Limit = 1,
                    }
                };
            });
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseAuthorization();
            app.UseClientRateLimiting();

            app.MapControllers();

            app.Run();
        }
    }
}
