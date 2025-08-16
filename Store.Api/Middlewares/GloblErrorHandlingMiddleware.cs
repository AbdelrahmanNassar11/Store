using Domain.Exceptions;
using Shared.ErrorModels;
using System.Diagnostics.CodeAnalysis;

namespace Store.Api.Middlewares
{
    public class GloblErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GloblErrorHandlingMiddleware> _logger;

        public GloblErrorHandlingMiddleware(RequestDelegate next, ILogger<GloblErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    await HandlingNotFoundEndPoint(context);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //1. Set Status Code for Response
                //2. Set Content Type for Response
                //3. Set Response Body
                //return Response
                //context.Response.StatusCode = ex switch
                //{
                //    NotFoundExceptions => StatusCodes.Status404NotFound,
                //    _ => StatusCodes.Status500InternalServerError
                //};
                await HandlingErrorAsync(context, ex);
            }
        }

        private static async Task HandlingErrorAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails
            {
                StatusCode = StatusCodes.Status500InternalServerError,

                ErrorMessage = ex.Message
            };

            response.StatusCode = ex switch
            {
                NotFoundExceptions => StatusCodes.Status404NotFound,
                BadHttpRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            context.Response.StatusCode = response.StatusCode;

            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandlingNotFoundEndPoint(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"End Point {context.Request.Path} is Not Found"
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
