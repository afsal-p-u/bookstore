using System.Net;
using System.Text.Json;

namespace BookStore.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) 
            {
                //custom error handling
                //context.Response.StatusCode = 500;

                //var result = JsonSerializer.Serialize(new
                //{
                //    Success = false,
                //    Error = ex.Message
                //});

                //return context.Response.WriteAsync(result);

                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new
            {
                statusCode = context.Response.StatusCode,
                message = ex.Message
            });

            return context.Response.WriteAsync(result);
        }
    }
}
