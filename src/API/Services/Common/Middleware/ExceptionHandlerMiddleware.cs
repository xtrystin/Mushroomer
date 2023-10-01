using Common.Exception;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Common.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (System.Exception ex)
        {
            await HandleException(ex, context.Response);
        }
    }

    private async Task HandleException(System.Exception ex, HttpResponse response)
    {
        try
        {
            if (ex is ApiException)
            {
                ApiException apiEx = ex as ApiException;
                response.StatusCode = (int)apiEx.StatusCode;

                response.ContentType = "text/plain";
                response.WriteAsync(apiEx.Message);
            }
            else
            {
                // todo: write exception to logs
                // if not production
                     //throw ex;
                response.StatusCode = 500;
                response.ContentType = "text/plain";
                await response.WriteAsync("Unhandled exception occurred");                       
            }
        }
        catch (System.Exception responseWritingException)
        {
            // todo: log this and outer exception
            // if not production
            //throw ex;
        }

    }
}

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        => builder.UseMiddleware<ExceptionHandlerMiddleware>();
}