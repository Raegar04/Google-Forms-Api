using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;

namespace GoogleFormsApi.Middlewares
{
    public class AppExceptionHandlerMiddleware
    {
        private readonly ILogger<AppExceptionHandlerMiddleware> _logger;

        private readonly RequestDelegate _next;

        public AppExceptionHandlerMiddleware(RequestDelegate next, ILogger<AppExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during executing {context}", context.Request.Path.Value);
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var message = JsonConvert.SerializeObject(new { message = ex.Message });
                await response.WriteAsync(ex.Message);
            }
        }
    }
}
