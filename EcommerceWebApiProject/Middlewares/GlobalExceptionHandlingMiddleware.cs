using System.Text.Json;

namespace EcommerceWebApiProject.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> logger;
        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception e)
            {
                logger.LogError(e, "An Unexpected error occured");

                var errorResponse = new
                {
                    StatusCode = 500,
                    Message = "Some Error Occured",
                    Details = e.Message,
                };

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var json = JsonSerializer.Serialize(errorResponse);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
