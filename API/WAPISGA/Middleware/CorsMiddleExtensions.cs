namespace WAPISGA.Middleware;

public static class CorsMiddleExtensions
{
    public static IApplicationBuilder UseCorsMiddle(this IApplicationBuilder pBuilder)
    {
        return pBuilder.UseMiddleware<CorsMiddle>();
    }
}
