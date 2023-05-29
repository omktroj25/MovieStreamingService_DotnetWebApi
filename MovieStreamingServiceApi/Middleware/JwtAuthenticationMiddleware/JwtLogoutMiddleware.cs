using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStreamingServiceApi.Middleware.JwtAuthenticationMiddleware;
public class JwtLogoutMiddleware
{
    private readonly RequestDelegate _next;

    public JwtLogoutMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check if the request is for logout
        if (context.Request.Path == "/api/auth/logout" && context.Request.Method == "POST")
        {
            // Get the token from the request
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                // Invalidate the token by adding it to the blacklist
                TokenBlacklist.AddToken(token);

                // Respond with a successful logout message
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync("Logout successful");
                return;
            }
        }

        // Continue processing the request
        await _next(context);
    }
}
