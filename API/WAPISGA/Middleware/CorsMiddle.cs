namespace WAPISGA.Middleware;

public class CorsMiddle
{
    private readonly RequestDelegate _next;

    public CorsMiddle(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        if (httpContext.Request.Method == "OPTIONS")
        {
            httpContext.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            httpContext.Response.Headers.Append("Access-Control-Allow-Credentials", "true");
            httpContext.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type, X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Content-Length, Content-MD5, Date, X-Api-Version, X-File-Name");
            httpContext.Response.Headers.Append("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");
            httpContext.Response.StatusCode = 200;
            return httpContext.Response.WriteAsync("OK");
        }
        else
        {
            httpContext.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            httpContext.Response.Headers.Append("Access-Control-Allow-Credentials", "true");
            httpContext.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type, X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Content-Length, Content-MD5, Date, X-Api-Version, X-File-Name");
            httpContext.Response.Headers.Append("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");
        }

        return _next(httpContext);
    }
}
