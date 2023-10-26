using System.Globalization;

namespace Middleware.Example;

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Response.StatusCode == 401)
        {
            if (context.Response.Headers.WWWAuthenticate.Count > 0)
            {
                await context.Response.WriteAsJsonAsync(new {
                    Code = 401,
                    Message = "You need to be Authenticated",
                    Data = new {}
                });
                return;
            }
        }

        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }
}
