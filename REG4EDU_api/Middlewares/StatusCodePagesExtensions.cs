using Microsoft.AspNetCore.Mvc;

namespace REG4EDU_api.Middlewares
{
    public static class StatusCodePagesExtensions
    {
        public static void UseCustomStatusCodePages(this IApplicationBuilder app)
        {
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    response.ContentType = "application/json";
                    var problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Title = "Unauthorized",
                        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
                        Detail = "You are not authorized to access this resource."
                    };

                    await response.WriteAsJsonAsync(problemDetails);
                }
            });
        }
    }
}
