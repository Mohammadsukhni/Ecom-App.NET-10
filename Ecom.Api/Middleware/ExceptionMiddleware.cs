using Ecom.Api.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ecom.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly IMemoryCache memeoryCache;
        private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);

        public ExceptionMiddleware(RequestDelegate next, IMemoryCache memoryCache, IHostEnvironment env)
        {
            _next = next;
            memeoryCache = memoryCache;
            _env = env;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                ApplySecurity(context);
                if (!isRequestAllowd(context))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";
                    var response = new ApiException((int)HttpStatusCode.TooManyRequests, "Too many requests. Please try again later.");
                    await context.Response.WriteAsJsonAsync(response);
                    return;
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = _env.IsDevelopment() ?
                    new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new ApiException((int)HttpStatusCode.InternalServerError, ex.Message);
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
        private bool isRequestAllowd(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress.ToString();
            var cacheKey = $"Rate:{ipAddress} ";
            var dateNow = DateTime.UtcNow;
            var (timesTamp, count) = memeoryCache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
                return (timesTamp: dateNow, count: 0);
            });
            if (dateNow - timesTamp < _rateLimitWindow)
            {
                if (count >= 8)
                {
                    return false;
                }
                memeoryCache.Set(cacheKey, (timesTamp, count + 1), _rateLimitWindow);

            }
            else
            {
                memeoryCache.Set(cacheKey, (timesTamp, count), _rateLimitWindow);

            }
            return true;
        }
        private void ApplySecurity(HttpContext context)
        { 
            context.Response.Headers["X-Content-Type-Options"] ="nosniff";
            context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
            context.Response.Headers["X-Frame-Options"] = "DENY";
        }
    }
}
