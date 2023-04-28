using Dapper;
using Npgsql;
using System.Text;

namespace WebAPI.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
    {
        try
        {
            RequestLogModel log = GetLogData(context);
            await SaveLRequestogToDb(configuration, log);
        }
        catch
        {
            //todo: log error
        }

        await _next(context);
    }

    private RequestLogModel GetLogData(HttpContext context)     //todo: log body 8 auth header
    {
        var clientIP = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
        var host = context.Request.Host.ToString();
        var path = context.Request.Path;
        var method = context.Request.Method;
        var query = GetRequestQueryData(context);

        return new RequestLogModel(clientIP, host, path, method, query, DateTime.Now);
    }

    private record RequestLogModel(string ClientIP, string Host, string Path, string Method, string Query, DateTime CreatedDate);

    private string GetRequestQueryData(HttpContext context)
    {
        StringBuilder output = new();
        foreach (var item in context.Request.Query)
        {
            output.AppendFormat("{0}: {1}\n", item.Key, item.Value);
        }

        return output.ToString();
    }

    private async Task SaveLRequestogToDb(IConfiguration configuration, RequestLogModel log)
    {
        var connectionString = configuration["ConnectionStrings:PostgresLogsDb"];
        using var connection = new NpgsqlConnection(connectionString);

        var sql = "INSERT INTO Logs(IP, Path, Host, Method, Query, CreatedDate) VALUES(@ClientIP, @Path, @Host, @Method, @Query, @CreatedDate);";
        
        try
        {
            await connection.ExecuteAsync(sql, log);
        }
        catch (Exception)
        {
            //todo
            //_logger.LogError("Request log was not successfully saved to db")
        }
    }
}

public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLoggingMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestLoggingMiddleware>();
    }
}