using System.Net;
using System.Text.Json;

namespace StaySphere.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(
        HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 
            (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            status = 500,
            message = "Something went wrong!",
            detail = ex.Message
        };

        return context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}