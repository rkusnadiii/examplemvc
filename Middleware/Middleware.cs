using Microsoft.AspNetCore.Http.Extensions;

namespace examplemvc.Middleware;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Value != "/login")
        {
            await context.Response.WriteAsJsonAsync(new {msg = " bukan login "});
        }
        else {
            await _next(context);
        }
    }
}