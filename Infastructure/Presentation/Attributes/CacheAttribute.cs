using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class CacheAttribute(int durationInSecend) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;
            var cacheKey = GenerateCacheKey(context.HttpContext.Request);
            var result = await cacheService.GetCacheValueAsync(cacheKey);
            if(!string.IsNullOrEmpty(result))
            {
                //Return Response
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status200OK,
                    ContentType = "application/json",
                    Content = result
                };
                return;
            }
          

            //Excute The End Point
            var contextResult = await next.Invoke();
            if(contextResult.Result is OkObjectResult okObject)
            {
                await cacheService.SetCacheValueAsync(cacheKey, okObject.Value, TimeSpan.FromSeconds(durationInSecend));
            }

        }
        private string GenerateCacheKey(HttpRequest request) 
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            foreach(var query in request.Query.OrderBy(P => P.Key))
            {
               key.Append($"|{query.Key}-{query.Value}");
            }
            return key.ToString();
        }

    }
}
