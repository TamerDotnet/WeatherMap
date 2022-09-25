using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WeatherMap.API.Settings;

namespace WeatherMap.API.Attributes
{
    public class ApiKeyRequiredAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "APIKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        { 
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Result = GetInvalidContentResult();            
                return;
            }
 
             
            if (!ApiKeys.AvailableKeysToChooseFrom.Contains(extractedApiKey.ToString()))
            {
                context.Result = GetInvalidContentResult();
                return;
            }
            await next();
        }

        private ContentResult GetInvalidContentResult()
        {
            return new ContentResult()
            {
                StatusCode = 401,
                Content = "Api Key is not valid"
            };
        }
    }
}
